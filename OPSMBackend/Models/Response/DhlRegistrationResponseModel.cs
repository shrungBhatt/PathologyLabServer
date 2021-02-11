using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class DhlRegistrationResponseModel
    {
        public List<HdlRegistration> HdlRegistrations { get; set; }
        public List<RegistrationCategories> Categories { get; set; }
        public List<RegistrationTypes> RegistrationTypes { get; set; }
    }
}
