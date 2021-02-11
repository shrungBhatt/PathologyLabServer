using OPSMBackend.DataEntities;
using OPSMBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class HdlReferringCutReportResponseModel
    {

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<HdlReferringCutModelDto> HdlReferringCutModelDtos { get; set; } = new List<HdlReferringCutModelDto>();
        public List<HdlRegistration> Hdls { get; set; }

    }
}
