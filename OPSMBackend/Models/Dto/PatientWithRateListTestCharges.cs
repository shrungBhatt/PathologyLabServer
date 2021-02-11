using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class PatientWithRateListTestCharges
    {
        public int PatientId { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public float TotalCharges
        {
            get
            {
                float value = 0;
                if(TestTitles != null)
                {
                    foreach(var test in TestTitles)
                    {
                        value += test.Charges;
                    }
                }
                return value;
            }
        }
        public List<TestTitles> TestTitles { get; set; } = new List<TestTitles>();
    }
}
