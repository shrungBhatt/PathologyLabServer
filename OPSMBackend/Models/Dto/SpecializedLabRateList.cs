using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class SpecializedLabRateList
    {
        public HdlRegistration HdlRegistration { get; set; }
        public List<SpecializedLabRateList> SpecializedLabRateLists { get; set; }
    }
}
