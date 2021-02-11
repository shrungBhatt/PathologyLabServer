using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class FieldOptions : BaseEntity
    {
        public FieldOptions()
        {
            Employees = new HashSet<Employees>();
            PatientBillPayment = new HashSet<PatientBillPayment>();
            HdlBillPayment = new HashSet<HdlBillPayment>();
        }

        public int Id { get; set; }
        public int FieldId { get; set; }
        public string Name { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [IgnoreCopy]
        public virtual Fields Field { get; set; }
        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<PatientBillPayment> PatientBillPayment { get; set; }

        [JsonIgnore]
        [IgnoreCopy]
        public virtual ICollection<Patient> Patient { get; set; }

        public virtual ICollection<HdlBillPayment> HdlBillPayment { get; set; }
    }
}
