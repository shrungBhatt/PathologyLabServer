using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class ExpensesResponseModel
    {
        public List<Expenses> Expenses { get; set; }
        public List<ExpensesAccountHead> ExpensesAccountHeads { get; set; }
    }
}
