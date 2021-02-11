using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OPSMBackend.DataEntities
{
    public partial class PatientBill : BaseEntity
    {
        public PatientBill()
        {
            PatientBillPayment = new Collection<PatientBillPayment>();
        }
        public int Id { get; set; }
        public double TotalCharges { get; set; }
        public int PatientId { get; set; }
        public int? Discount { get; set; }
        public double? DiscountedAmount { get; set; }
        public double? Gst { get; set; }
        public double? AmountPaid { get; set; }
        public double? Balance { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? BillNo { get; set; }
        public DateTime? BillDate { get; set; }

        [IgnoreCopy]
        public virtual Patient Patient { get; set; }

        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<PatientBillPayment> PatientBillPayment { get; set; }
    }
}
