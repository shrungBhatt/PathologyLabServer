using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class TestResultsResponseModel
    {
        public List<Patient> Patients { get; set; }
        public List<SignaturePrototypes> Signatures { get; set; }
    }
}
