using OPSMBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models
{
    public class UserListResponseModel
    {
        public List<UsersDto> Users { get; set; }

        public List<RoleTypeDto> RoleTypes { get; set; }
    }
}
