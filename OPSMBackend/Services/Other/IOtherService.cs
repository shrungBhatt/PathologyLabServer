using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Other
{
    public interface IOtherService
    {
        IEnumerable<Initials> GetInitials();
        IEnumerable<Genders> GetGenders();
        IEnumerable<FieldOptions> GetFieldOptions();
        IEnumerable<RegistrationTypes> GetReferredByTypes();
        void InsertInitial(Initials initial);
        void InserGenders(Genders gender);
        void UpdateInitial(Initials initial);
        void UpdateGender(Genders genders); 
        void DeleteInitial(int id);
        void DeleteGender(int id);
    }
}
