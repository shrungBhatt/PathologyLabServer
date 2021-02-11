using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class PatientBillPayment : BaseEntity
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ReceiptNo { get; set; }
        public bool? CashMode { get; set; }
        public bool? ChequeMode { get; set; }
        public bool? CardMode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNo { get; set; }
        public string CardNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? Balance { get; set; }
        public string BillPaidBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int PaymentType { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public virtual PatientBill Bill { get; set; }

        public virtual FieldOptions PaymentTypeNavigation { get; set; }
    }
}
