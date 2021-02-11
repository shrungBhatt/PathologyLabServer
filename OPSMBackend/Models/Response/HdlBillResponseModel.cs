using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class HdlBillResponseModel
    {
        public List<HdlRegistration> HdlRegistrations { get; set; } = new List<HdlRegistration>();
    }
}
