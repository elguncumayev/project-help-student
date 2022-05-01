using ProjectCore.DTOs;
using ProjectCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCore.Services
{
    public interface IUserService
    {
        Task<int> CreateAsync(UserRegisterDto user);

        Task UpdateAsync(User user);

        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetManyByIdsAsync(List<int> ids);
        Task<User> GetByIdAsync(int Id);
        Task<User> GetByEmailAsync(string Email);
        Task<User> GetByUsernameAsync(string Username);
        Task<User> GetByLoginInfoAsync(UserLoginDto userLoginDto);
        Task<User> GetByRefreshTokenAsync(string refreshToken);

        Task<AuthenticateResponse> AuthenticateAsync(User user, string ipAddress);
        Task<AuthenticateResponse> RefreshTokenAsync(User user, string token, string ipAddress);

        Task<bool> RevokeTokenAsync(User user, string token, string ipAddress);
    }
}
