using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class SpecializedLabSamples : BaseEntity
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public int PatientId { get; set; }
        public int TestTitleId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [NotMapped]
        public string LabName { get; set; }
        [NotMapped]
        public string TestName { get; set; }

        public virtual HdlRegistration Lab { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual TestTitles TestTitle { get; set; }
    }
}
