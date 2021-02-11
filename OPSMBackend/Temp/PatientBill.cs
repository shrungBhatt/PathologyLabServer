using System;
using System.Collections.Generic;

namespace OPSMBackend.Temp
{
    public partial class PatientBill
    {
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

        public virtual Patient Patient { get; set; }
    }
}
