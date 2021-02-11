using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class TestReagentRelation : BaseEntity
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int OtherTestId { get; set; }
        public int Unit { get; set; }
        public int QtyPerTest { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual OtherTests OtherTest { get; set; }
        public virtual Reagents Reagent { get; set; }
    }
}
