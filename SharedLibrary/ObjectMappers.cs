using ProjectCore.DTOs;
using ProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public static class ObjectMappers
    {
        public static UserDTO AsUserDTO(this User user)
        {
            return new UserDTO {
                ID = user.ID,
                Email = user.Email,
                FirstName = user.FirstName,
                FamilyName = user.Surname,
                Role = user.Role,
                ProfilePhotoURL = user.ProfilePhotoURL
            };
        }
    }
}
