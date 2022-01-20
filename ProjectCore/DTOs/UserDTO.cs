using ProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Role { get; set; }
        public string ProfilePhotoURL { get; set; }
    }
}
