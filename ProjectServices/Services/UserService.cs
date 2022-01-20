using ProjectCore.DTOs;
using ProjectCore.Models;
using ProjectCore.Repositories;
using ProjectCore.Services;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUser(User user)
        {
            await _repository.CreateAsync(user);
        }

        public async Task<UserDTO> GetUserDto(int ID)
        {
            return (await _repository.GetAsync(ID)).AsUserDTO();
        }
        public async Task<User> GetUserByLoginInfo(UserLoginDto userLoginDto)
        {
            return await _repository.GetByFilterAsync(user => user.Email == userLoginDto.Email && user.Password == userLoginDto.Password);
        }

        public async Task<IEnumerable<User>> GetAllUsersDtos()
        {
            return await _repository.GetAllAsync();
        }

    }
}
