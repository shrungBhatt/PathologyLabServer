using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Roletypes
{
    public interface IRoleTypesService
    {
        IEnumerable<RoleTypes> GetRoleTypes();

    }
}
