using System;
using System.Collections.Generic;

namespace OPSMBackend.Temp
{
    public partial class Patient
    {
        public Patient()
        {
            PatientBill = new HashSet<PatientBill>();
        }

        public int Id { get; set; }
        public string PatientCode { get; set; }
        public int? InitialId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AgeInYears { get; set; }
        public int? AgeInMonths { get; set; }
        public int? AgeInDays { get; set; }
        public int? Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Occupation { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ReferredBy { get; set; }
        public string Address { get; set; }
        public int? CivilStatus { get; set; }
        public bool IsFinished { get; set; }

        public virtual ICollection<PatientBill> PatientBill { get; set; }
    }
}
