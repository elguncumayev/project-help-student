using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.DTOs
{
    public class UserLoginDto
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}
