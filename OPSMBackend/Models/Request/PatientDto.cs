using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Request
{
    public class PatientDto
    {

        public Patient Patient { get; set; }
        public List<TestTitles> SelectedTestTitles { get; set; }

        public PatientBill PatientBill { get; set; }

    }
}
