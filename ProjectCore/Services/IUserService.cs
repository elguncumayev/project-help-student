using ProjectCore.DTOs;
using ProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersDtos();
        Task<UserDTO> GetUserDto(int ID);
        Task<User> GetUserByLoginInfo(UserLoginDto userLoginDto);
        Task CreateUser(User user);
    }
}
