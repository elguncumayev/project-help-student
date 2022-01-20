using Microsoft.AspNetCore.Mvc;
using ProjectCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Services
{
    public interface IAuthentication
    {
        public Task<IActionResult> Login(UserLoginDto userLoginDTO);
    }
}
