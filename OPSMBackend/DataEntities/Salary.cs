using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Salary : BaseEntity
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public long AccountNo { get; set; }
        public long BasicSalary { get; set; }
        public long LearningAllowance { get; set; }
        public long Hra { get; set; }
        public long CommunicationAllowance { get; set; }
        public long TransportationAllowance { get; set; }
        public long SalaryAmount { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int EmployeeId { get; set; }

        [IgnoreCopy]
        public virtual Employees Employee { get; set; }
    }
}
