using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class AccountHead : BaseEntity
    {
        public AccountHead()
        {
            OtherIncome = new HashSet<OtherIncome>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<OtherIncome> OtherIncome { get; set; }
    }
}
