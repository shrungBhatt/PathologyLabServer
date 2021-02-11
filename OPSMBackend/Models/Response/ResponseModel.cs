using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OPSMBackend.Models
{
    public class ResponseModel
    {
     
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
        
    }


    public class ResponseStatusCode
    {
        public const string SUCCESS = "success";
        public const string FAIL = "fail";
        public const string ERROR = "error";
    }

    public enum ErrorCodes
    {
        invalidData = 100,
        dataNotFound = 101,
        foreignConstraintFailed = 102
    }

}
