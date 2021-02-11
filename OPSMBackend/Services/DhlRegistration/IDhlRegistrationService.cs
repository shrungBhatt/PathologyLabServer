using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.DhlRegistration
{
    public interface IDhlRegistrationService
    {
        DhlRegistrationResponseModel GetDhlRegistrations();
        void InsertDhlRegistration(HdlRegistration hdlRegistration);
        void UpdateDhlRegistration(HdlRegistration hdlRegistration);
        void DeleteDhlRegistration(int id);

        RateListResponseModel GetRateLists();
        void InsertRateList(RateListModel rateList);
        void UpdateRateList(RateListModel rateList);
        void DeleteRateList(int id);
    }
}
