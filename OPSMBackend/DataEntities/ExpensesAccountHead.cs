using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class ExpensesAccountHead : BaseEntity
    {
        public ExpensesAccountHead()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreCopy]
        public virtual ICollection<Expenses> Expenses { get; set; }
    }
}
