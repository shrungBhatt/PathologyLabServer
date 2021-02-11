using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class HdlBill : BaseEntity
    {
        public HdlBill()
        {
            HdlBillPayment = new HashSet<HdlBillPayment>();
        }

        public int Id { get; set; }
        public int BillNo { get; set; }
        public int HdlId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime BillDate { get; set; }
        public float TotalCharges { get; set; }
        public float? Balance { get; set; }
        public double? Discount { get; set; }
        public double? Gst { get; set; }
        public float? AmountPaid { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual HdlRegistration Hdl { get; set; }
        public virtual ICollection<HdlBillPayment> HdlBillPayment { get; set; }
    }
}
