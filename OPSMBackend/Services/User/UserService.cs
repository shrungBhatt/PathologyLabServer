using OPSMBackend.DataEntities;
using OPSMBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.User
{
    public class UserService : IUserService
    {

        private IRepository<Users> usersRepository;

        public UserService(IRepository<Users> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public void DeleteUser(int id)
        {
            usersRepository.Delete(usersRepository.Get(id));
        }

        public IEnumerable<Users> GetUsers()
        {
            return usersRepository.GetAll().ToList();
        }

        public Users GetUser(int id)
        {
            return usersRepository.Get(id);
        }

        public void InsertUser(Users user)
        {
            usersRepository.Insert(user);
        }

        public void UpdateUser(Users user)
        {
            usersRepository.Update(user);
        }
    }
}
