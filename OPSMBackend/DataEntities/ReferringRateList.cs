using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class ReferringRateList : BaseEntity
    {
        public int Id { get; set; }
        public int HdlId { get; set; }
        public int TestTitleId { get; set; }
        public double? ReferringAmount { get; set; }
        public double? ReferringPercentage { get; set; }

        [IgnoreCopy]
        public virtual HdlRegistration Hdl { get; set; }
        [IgnoreCopy]
        public virtual TestTitles TestTitle { get; set; }
    }
}
