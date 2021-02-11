using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class TestGroups : BaseEntity
    {
        public TestGroups()
        {
            OtherTests = new HashSet<OtherTests>();
            TestTitles = new HashSet<TestTitles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int OrderNo { get; set; }
        public bool? ShowTitleInReports { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<OtherTests> OtherTests { get; set; }

        [JsonIgnore]
        public virtual ICollection<TestResults> TestResults { get; set; }

        [JsonIgnore]
        public virtual ICollection<TestTitles> TestTitles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Formulas> Formulas { get; set; }
    }
}
