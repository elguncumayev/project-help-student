using Microsoft.Extensions.Configuration;
using ProjectCore.DTOs;
using ProjectCore.Models;
using ProjectCore.Repositories;
using ProjectCore.Services;
using ProjectServices.Auth;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectServices.Services
{
    public class UserService : IUserService
    {
        //TODO - IMPORTANT : Change methods with jwtUtils help and maintain Single Responsibility
        private readonly IUserRepository _repository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IJwtUtils jwtUtils, IConfiguration configuration)
        {
            _repository = repository;
            _jwtUtils = jwtUtils;
            _configuration = configuration;
        }


        public async Task<int> CreateAsync(UserRegisterDto userRegisterDto)
        {
            User newUser = userRegisterDto.AsUser();
            newUser.Role = "player";
            newUser.CreatedTime = DateTimeOffset.Now;
            return await _repository.CreateAsync(newUser);
        }


        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return new List<User>();
            await Task.CompletedTask;
            //throw new NotImplementedException();
            //return await _repository.GetAllAsync();
        }
        public async Task<IEnumerable<User>> GetManyByIdsAsync(List<int> ids)
        {
            return await _repository.GetManyByFilterAsync(user => ids.Contains(user.ID));
        }


        public async Task<User> GetByIdAsync(int Id)
        {
            return await _repository.GetSingleByFilterAsync(user => user.ID == Id);
        }
        public async Task<User> GetByEmailAsync(string Email)
        {
            try
            {
                return await _repository.GetSingleByFilterAsync(user => user.Email == Email);
            }
            catch
            {
                Console.WriteLine($"Error with {Email} address");
                return await Task.FromResult(new User());
            }
        }
        public async Task<User> GetByUsernameAsync(string Username)
        {
            return await _repository.GetSingleByFilterAsync(user => user.Username == Username);
        }
        public async Task<User> GetByLoginInfoAsync(UserLoginDto userLoginDto)
        {
            return await _repository.GetSingleByFilterAsync(user => (user.Email == userLoginDto.EmailOrUsername || user.Username == userLoginDto.EmailOrUsername) && user.Password == userLoginDto.Password);
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _repository.GetSingleByFilterAsync(user => user.RefreshTokens.Any(r => r.Token.Equals(refreshToken)));
        }


        public async Task<AuthenticateResponse> AuthenticateAsync(User user, string ipAddress)
        {
            string accessToken = _jwtUtils.GenerateAccessToken(user);
            RefreshToken refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);

            user.RefreshTokens.Add(refreshToken);

            RemoveOldRefreshTokens(user);

            await _repository.UpdateAsync(user);

            return new AuthenticateResponse(user, accessToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshTokenAsync(User user, string token, string ipAddress)
        {
            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                await _repository.UpdateAsync(user);
            }

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            RemoveOldRefreshTokens(user);

            await _repository.UpdateAsync(user);

            string jwtToken = _jwtUtils.GenerateAccessToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }


        public async Task<bool> RevokeTokenAsync(User user, string token, string ipAddress)
        {
            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReasonRevoked = "Revoked without replacement";
            refreshToken.ReplacedByToken = null;

            await _repository.UpdateAsync(user);
            return true;
        }


        private void RemoveOldRefreshTokens(User user)
        {
            // Remove old inactive refresh tokens from user based on TTL in appsettings.json
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(int.Parse(_configuration["RefreshTokenTTL"])) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(ref childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void RevokeRefreshToken(ref RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            RevokeRefreshToken(ref refreshToken, ipAddress, "Replaced by new token", _jwtUtils.GenerateRefreshToken(ipAddress).Token);
            return _jwtUtils.GenerateRefreshToken(ipAddress);
        }

    }
}
