using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class EmployeeCategories : BaseEntity
    {
        public EmployeeCategories()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string EmployeeCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
