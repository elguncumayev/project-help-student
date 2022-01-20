using Microsoft.AspNetCore.Mvc;
using ProjectCore.DTOs;
using ProjectCore.Models;
using ProjectCore.Repositories;
using ProjectCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Services
{
    public class AuthService : IAuthentication
    {
        private readonly IUserRepository _repository;
        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<IActionResult> Login(UserLoginDto userLoginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
