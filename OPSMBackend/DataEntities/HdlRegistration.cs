using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class HdlRegistration : BaseEntity
    {
        public HdlRegistration()
        {
            MonthlyRateList = new HashSet<MonthlyRateList>();
            ReferringRateList = new HashSet<ReferringRateList>();
            SpecializedLabRateList = new HashSet<SpecializedLabRateList>();
            HdlBill = new HashSet<HdlBill>();
            SpecializedLabSamples = new HashSet<SpecializedLabSamples>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegistrationTypeId { get; set; }
        public int RegistrationCategoryId { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AdditionalNotes { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public HdlBill Bill { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public virtual RegistrationCategories RegistrationCategory { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public virtual RegistrationTypes RegistrationType { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<MonthlyRateList> MonthlyRateList { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<ReferringRateList> ReferringRateList { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<SpecializedLabRateList> SpecializedLabRateList { get; set; }
        public virtual ICollection<HdlBill> HdlBill { get; set; }
        public virtual ICollection<SpecializedLabSamples> SpecializedLabSamples { get; set; }
    }
}
