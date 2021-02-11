using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPSMBackend.Models;

namespace OPSMBackend.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected object GetResponse(ResponseType responseType, string resultCode, object data = null)
        {
            var responseModel = new ResponseModel();
            responseModel.Status = resultCode;
            object responseData = new object();

            switch (responseType)
            {
                case ResponseType.ACK:
                    responseData = new JObject() { new JProperty("acknowledgement", JToken.FromObject(GetAck(resultCode))) };
                    break;
                case ResponseType.ERROR:
                case ResponseType.FAIL:
                    responseData = new JObject() { new JProperty("error", JToken.FromObject(data)) };
                    break;
                case ResponseType.OBJECT:
                    responseData = data;
                    break;
            }

            responseModel.Data = responseData;

            return responseModel;
        }

        protected AckModel GetAck(string statusCode)
        {
            return new AckModel { Status = statusCode.ToString() };
        }

        protected ErrorModel GetError(ErrorCodes errorCode, string title, string message)
        {
            return new ErrorModel { ErrorCode = (int)errorCode, ErrorTitle = title, ErrorMessage = message };
        }
    }

    public enum ResponseType
    {
        ACK,
        FAIL,
        ERROR,
        OBJECT
    }
}
