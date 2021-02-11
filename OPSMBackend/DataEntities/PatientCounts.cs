using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class PatientCounts : BaseEntity
    {
        public int Id { get; set; }
        public int YearName { get; set; }
        public int MonthName { get; set; }
        public int PatientCount { get; set; }
    }
}
