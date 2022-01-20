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
        public Role UserRole { get; set; }
        public string ProfilePhotoURL { get; set; }
    }
}
