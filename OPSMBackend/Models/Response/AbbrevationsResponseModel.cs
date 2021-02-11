using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class AbbrevationsResponseModel
    {
        public List<Abbrevations> Abbrevations { get; set; }
        public List<OtherTests> OtherTests { get; set; }
    }
}
