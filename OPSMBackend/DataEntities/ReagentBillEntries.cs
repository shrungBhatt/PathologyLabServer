using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class ReagentBillEntries : BaseEntity
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public int ReagentId { get; set; }
        public int BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsPaid { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreCopy]
        public virtual Dealers Dealer { get; set; }

        [IgnoreCopy]
        public virtual Reagents Reagent { get; set; }
    }
}
