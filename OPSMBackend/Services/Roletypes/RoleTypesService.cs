using OPSMBackend.DataEntities;
using OPSMBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Roletypes
{
    public class RoleTypesService : IRoleTypesService
    {
        private IRepository<RoleTypes> roleTypesRepository;


        public RoleTypesService(IRepository<RoleTypes> roleTypesRespository)
        {
            this.roleTypesRepository = roleTypesRespository;
        }

        public IEnumerable<RoleTypes> GetRoleTypes()
        {
            return roleTypesRepository.GetAll().ToList();
        }
    }
}
