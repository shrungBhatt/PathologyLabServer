using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class RateListModel
    {
        public HdlRegistration HdlRegistration { get; set; }
        public List<SpecializedLabRateList> SpecializedLabRateLists { get; set; } = new List<SpecializedLabRateList>();
        public List<ReferringRateList> ReferredRateLists { get; set; } = new List<ReferringRateList>();
        public List<MonthlyRateList> MonthlyRateLists { get; set; } = new List<MonthlyRateList>();


    }
}
