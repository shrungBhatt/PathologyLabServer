using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Formulas : BaseEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Formula { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? TitleId { get; set; }
        public int? GroupId { get; set; }

        [JsonIgnore]
        public virtual OtherTests Test { get; set; }
        
        [JsonIgnore]
        public virtual TestGroups Group { get; set; }

        [JsonIgnore]
        public virtual TestTitles Title { get; set; }
    }
}
