﻿using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Expenses : BaseEntity
    {
        public int Id { get; set; }
        public int BillNo { get; set; }
        public string NatureOfWork { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal Amount { get; set; }
        public bool? CashMode { get; set; }
        public bool? ChequeMode { get; set; }
        public bool? CardMode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string CardNo { get; set; }
        public int AccountHeadId { get; set; }
        public string PaidBy { get; set; }
        public string VoucherNo { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        [IgnoreCopy]
        public virtual ExpensesAccountHead AccountHead { get; set; }
    }
}
