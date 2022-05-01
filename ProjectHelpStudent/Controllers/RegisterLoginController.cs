using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCore.DTOs;
using ProjectCore.Models;
using ProjectCore.Services;
using ProjectServices.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHelpStudent.Controllers
{
    [ApiController]
    public class RegisterLoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public RegisterLoginController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/register")]
        [MyAllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto userRegisterDto)
        {
            User userFromReg = await _userService.GetByEmailAsync(userRegisterDto.Email);
            if (userFromReg != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(userFromReg));
                return BadRequest("User exist");
            }
            int newUserId = await _userService.CreateAsync(userRegisterDto);
            return Ok(newUserId);
        }


        [Route("api/login")]
        [MyAllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userLoginDTO)
        {
            Console.WriteLine("Login attempt. User : " + JsonConvert.SerializeObject(userLoginDTO));

            User user = await _userService.GetByLoginInfoAsync(userLoginDTO) ?? null;

            if (user != null) return Ok(await _userService.AuthenticateAsync(user, GetIpAddress()));

            return NotFound("User not found!");
        }


        [Route("api/refresh-token")]
        [MyAllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            if (refreshToken.Equals(string.Empty) || refreshToken == null)
            {
                return BadRequest(new { message = "Token is required" });
            }
            User user = await _userService.GetByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                return BadRequest("Invalid refresh token!");
            }
            else
            {
                RefreshToken token = user.RefreshTokens.Single(x => x.Token == refreshToken);

                if (!token.IsActive) return BadRequest("Invalid refresh token!");

                return Ok(await _userService.RefreshTokenAsync(user, refreshToken, GetIpAddress()));
            }
        }


        [Route("api/revoke-token")]
        [MyAuthorize]
        [HttpPost]
        public async Task<IActionResult> RevokeToken(string token)
        {
            if (token.Equals(string.Empty) || token == null)
            {
                return BadRequest(new { message = "Token is required" });
            }
            User user = await _userService.GetByRefreshTokenAsync(token);
            if (user == null)
            {
                return BadRequest("Invalid token");
            }
            bool res = await _userService.RevokeTokenAsync(user, token, GetIpAddress());
            return res ? Ok(new { message = "Token revoked" }) : BadRequest("Invalid token");
        }

        private string GetIpAddress()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
