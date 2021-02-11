using Newtonsoft.Json;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class UsersDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailId { get; set; }
        public int? RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string Specialization { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


        public UsersDto GetUsersDto(Users user)
        {
            var userDto = new UsersDto();
            userDto.Degree = user.Degree;
            userDto.EmailId = user.EmailId;
            userDto.FirstName = user.FirstName;
            userDto.Id = user.Id;
            userDto.LastName = user.LastName;
            userDto.MiddleName = user.MiddleName;
            userDto.ModifiedBy = user.ModifiedBy;
            userDto.ModifiedDate = user.ModifiedDate;
            userDto.Password = user.Password;
            userDto.RoleId = user.RoleId;
            userDto.Specialization = user.Specialization;
            userDto.UserName = user.UserName;

            return userDto;
        }

        public Users GetUser(UsersDto usersDto)
        {
            var user = new Users();
            user.Degree = usersDto.Degree;
            user.EmailId = usersDto.EmailId;
            user.FirstName = usersDto.FirstName;
            user.Id = usersDto.Id;
            user.LastName = usersDto.LastName;
            user.MiddleName = usersDto.MiddleName;
            user.ModifiedBy = usersDto.ModifiedBy;
            user.ModifiedDate = usersDto.ModifiedDate;
            user.Password = usersDto.Password;
            user.RoleId = usersDto.RoleId;
            user.Specialization = usersDto.Specialization;
            user.UserName = usersDto.UserName;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = usersDto.ModifiedBy;

            return user;
        }

        public Users GetUser(Users user, UsersDto usersDto)
        {
            user.Degree = usersDto.Degree;
            user.EmailId = usersDto.EmailId;
            user.FirstName = usersDto.FirstName;
            user.Id = usersDto.Id;
            user.LastName = usersDto.LastName;
            user.MiddleName = usersDto.MiddleName;
            user.ModifiedBy = usersDto.ModifiedBy;
            user.ModifiedDate = usersDto.ModifiedDate;
            user.Password = usersDto.Password;
            user.RoleId = usersDto.RoleId;
            user.Specialization = usersDto.Specialization;
            user.UserName = usersDto.UserName;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = usersDto.ModifiedBy;

            return user;
        }
    }
}
