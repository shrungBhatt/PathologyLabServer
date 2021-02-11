
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class MonthlyRateListDto
    {
        public HdlRegistration HdlRegistration { get; set; }
        public List<MonthlyRateList> MonthlyRateLists { get; set; }
    }
}
