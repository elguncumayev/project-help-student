using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCore.DTOs;
using ProjectCore.Models;
using ProjectCore.Services;
using ProjectServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHelpStudent.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        //[HttpGet]
        //public async Task<IEnumerable<User>> GetAll()
        //{
        //    return await _userService.GetAllUsersDtos();
        //}

        // GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public async Task<UserPublicDTO> Get(int id)
        //{
        //    return await _userService.GetUserDto(id);
        //}

        // POST api/<UserController>
        //[HttpPost]
        //public async Task Post([FromBody] User user)
        //{
        //    await _userService.CreateUser(user);
        //}

        // PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
