using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Fields : BaseEntity
    {
        public Fields()
        {
            FieldOptions = new HashSet<FieldOptions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<FieldOptions> FieldOptions { get; set; }
    }
}
