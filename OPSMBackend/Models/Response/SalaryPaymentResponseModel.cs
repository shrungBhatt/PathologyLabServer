using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class SalaryPaymentResponseModel
    {
        public List<Employees> Employees { get; set; }
        public List<SalaryPayment> SalaryPayments { get; set; }
    }
}
