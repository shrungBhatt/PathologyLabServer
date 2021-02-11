using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OPSMBackend.Models
{
    public class AckModel
    {

        [JsonProperty("status")]
        public string Status { get; set; }

    }
}
