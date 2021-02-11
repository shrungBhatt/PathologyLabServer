using OPSMBackend.DataEntities;
using OPSMBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Other
{
    public class OtherService : IOtherService
    {
        private readonly IRepository<Initials> initialsRepository;
        private readonly IRepository<Genders> gendersRepository;
        private readonly IRepository<FieldOptions> fieldOptionsRepository;
        private readonly IRepository<RegistrationTypes> registrationTypesRepository;

        public OtherService(IRepository<Initials> initialsRepository,
                            IRepository<Genders> gendersRepository,
                            IRepository<FieldOptions> fieldOptionsRepository,
                            IRepository<RegistrationTypes> registrationTypesRepository)
        {
            this.initialsRepository = initialsRepository;
            this.gendersRepository = gendersRepository;
            this.fieldOptionsRepository = fieldOptionsRepository;
            this.registrationTypesRepository = registrationTypesRepository;
        }

        public IEnumerable<RegistrationTypes> ReferredByTypes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public void DeleteGender(int id)
        {
            gendersRepository.Delete(gendersRepository.Get(id));
        }

        public void DeleteInitial(int id)
        {
            initialsRepository.Delete(initialsRepository.Get(id));
        }

        public IEnumerable<FieldOptions> GetFieldOptions()
        {
            return fieldOptionsRepository.GetAll().ToList();
        }

        public IEnumerable<Genders> GetGenders()
        {
            return gendersRepository.GetAll().ToList();
        }

        public IEnumerable<Initials> GetInitials()
        {
            return initialsRepository.GetAll().ToList();
        }

        public IEnumerable<RegistrationTypes> GetReferredByTypes()
        {
            return registrationTypesRepository.GetAll().ToList();
        }

        public void InserGenders(Genders gender)
        {
            gendersRepository.Insert(gender);
        }

        public void InsertInitial(Initials initial)
        {
            initialsRepository.Insert(initial);
        }

        public void UpdateGender(Genders genders)
        {
            gendersRepository.Insert(genders);
        }

        public void UpdateInitial(Initials initial)
        {
            initialsRepository.Update(initial);
        }
    }
}
