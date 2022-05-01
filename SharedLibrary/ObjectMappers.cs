using ProjectCore.DTOs;
using ProjectCore.Models;

namespace SharedLibrary
{
    public static class ObjectMappers
    {
        public static UserPublicDTO AsUserDTO(this User user)
        {
            return new UserPublicDTO {
                ID = user.ID,
                Email = user.Email,
                FirstName = user.Firstname,
                FamilyName = user.Lastname,
                Role = user.Role,
                ProfilePhotoURL = user.ProfilePhotoURL
            };
        }

        public static User AsUser(this UserRegisterDto userRegisterDto)
        {
            return new User
            {
                Username = userRegisterDto.Username.ToLower(),
                Email = userRegisterDto.Email.ToLower(),
                Firstname = userRegisterDto.Firstname,
                Lastname = userRegisterDto.Lastname,
                Password = userRegisterDto.Password
            };
        }
    }
}
