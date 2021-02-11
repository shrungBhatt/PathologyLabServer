using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class RoleTypeDto
    {

        public int Id { get; set; }
        public string Role { get; set; }


        public RoleTypeDto GetRoleTypeDto(RoleTypes roleTypes)
        {
            var dto = new RoleTypeDto();
            dto.Id = roleTypes.Id;
            dto.Role = roleTypes.Role;

            return dto;
        }
    }
}
