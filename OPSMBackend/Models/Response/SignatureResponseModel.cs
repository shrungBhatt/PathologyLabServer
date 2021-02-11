using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class SignatureResponseModel
    {
        public List<SignaturePrototypes> Signatures { get; set; }
    }
}
