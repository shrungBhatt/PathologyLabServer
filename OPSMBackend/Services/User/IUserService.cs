using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.User
{
    public interface IUserService
    {
        IEnumerable<Users> GetUsers();
        Users GetUser(int id);
        void InsertUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(int id);
    }
}
