using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class PatientBillPaymentReportDto
    {
        public int ReceiptNo { get; set; }
        public int BillPaymentId { get; set; }
        public int PatientId { get; set; } 
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public DateTime PaymentDate { get; set; } 
        public bool CashMode { get; set; }
        public bool ChequeMode { get; set; }
        public bool CardMode { get; set; } 
        public string PaymentType { get; set; }
        public int PaymentTypeId { get; set; }
        public float Amount { get; set; }
        public string PaidBy { get; set; }
        public string EntryDoneBy { get; set; }
        public FieldOptions BillPaymentType { get; set; }
    }
}
