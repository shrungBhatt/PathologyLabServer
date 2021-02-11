using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class SalaryResponseModel
    {
        public List<Employees> Employees { get; set; }
        public List<Salary> Salaries { get; set; }
    }
}
