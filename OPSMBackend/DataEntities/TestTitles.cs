using Microsoft.AspNet.OData.Query;
using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class TestTitles : BaseEntity
    {
        public TestTitles()
        {
            OtherTests = new HashSet<OtherTests>();
            MonthlyRateList = new HashSet<MonthlyRateList>();
            ReferringRateList = new HashSet<ReferringRateList>();
            SpecializedLabRateList = new HashSet<SpecializedLabRateList>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Charges { get; set; }
        public int? OrderBy { get; set; }
        public string HeaderNote { get; set; }
        public string FooterNote { get; set; }
        public bool WordFormatResult { get; set; }
        public bool ShowNormalRangeHeader { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [NotMapped, NotNavigable]
        [IgnoreCopy]
        public virtual TestGroups Group { get; set; }

        [JsonIgnore]
        public virtual ICollection<TestResults> TestResults { get; set; }

        [JsonIgnore]
        public virtual ICollection<OtherTests> OtherTests { get; set; }

        [JsonIgnore]
        public virtual ICollection<Formulas> Formulas { get; set; }
        [JsonIgnore]
        public virtual ICollection<MonthlyRateList> MonthlyRateList { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReferringRateList> ReferringRateList { get; set; }
        [JsonIgnore]
        public virtual ICollection<SpecializedLabRateList> SpecializedLabRateList { get; set; }
        public virtual ICollection<SpecializedLabSamples> SpecializedLabSamples { get; set; }
    }
}
