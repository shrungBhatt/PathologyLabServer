using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class ReagentEntryResponseModel
    {
        public List<Dealers> Dealers { get; set; }
        public List<Reagents> Reagents { get; set; }
        public List<ReagentBillEntries> ReagentEntries { get; set; }
    }
}
