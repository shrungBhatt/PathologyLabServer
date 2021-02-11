using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class HdlBillPayment : BaseEntity
    {
        public int Id { get; set; }
        public int ReceiptNo { get; set; }
        public int BillId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool? CashMode { get; set; }
        public bool? ChequeMode { get; set; }
        public bool? CardMode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNo { get; set; }
        public string CardNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public float PaymentAmount { get; set; }
        public float? Balance { get; set; }
        public int PaymentType { get; set; }
        public string BillPaidBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual HdlBill Bill { get; set; }
        public virtual FieldOptions PaymentTypeNavigation { get; set; }
    }
}
