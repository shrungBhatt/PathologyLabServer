using OPSMBackend.DataEntities;
using OPSMBackend.Models.Dto;
using OPSMBackend.Models.Request;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Patients = OPSMBackend.DataEntities.Patient;

namespace OPSMBackend.Services.Patient
{
    public interface IPatientService
    {
        IEnumerable<Patients> GetAllPatients();
        void UpdatePatient(Patients patient);
        void InsertPatient(Patients patient);
        void DeletePatient(int id);
        IEnumerable<PatientBill> GetPatientBill();
        void UpdatePatientBill(PatientBill patientBill);
        void InsertPatientBill(PatientBill patientBill);
        void DeletePatientBill(int id);
        void UpdatePatientTestResults(Patients patient);

        PatientWithRateListChargesRequest GetPatientWithRateListCharges(PatientWithRateListChargesRequest model);

        SpecializedLabSampleDto GetSpecializedLabSamples(SpecializedLabSampleDto specializedLabSampleDto);
        void UpdateSpecializedLabSamples(SpecializedLabSampleDto specializedLabSampleDto);

        SpecializedLabReportResponseModel GetSpecializedLabReport(SpecializedLabReportResponseModel model);
        HdlReferringCutReportResponseModel GetHdlReferringReport(HdlReferringCutReportResponseModel model);
    }
}
