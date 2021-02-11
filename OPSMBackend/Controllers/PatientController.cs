using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPSMBackend.DataEntities;
using OPSMBackend.Models;
using OPSMBackend.Models.Dto;
using OPSMBackend.Models.Request;
using OPSMBackend.Models.Response;
using OPSMBackend.Services.DhlRegistration;
using OPSMBackend.Services.Maintenance;
using OPSMBackend.Services.Other;
using OPSMBackend.Services.Patient;
using OPSMBackend.Services.Tests;

namespace OPSMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseController
    {

        private readonly IPatientService patientService;
        private readonly IOtherService otherService;
        private readonly ITestsService testService;
        private readonly IDhlRegistrationService dhlRegistrationService;
        private readonly IMaintenanceService maintenanceService;

        public PatientController(IPatientService patientService,
                                 IOtherService otherService,
                                 ITestsService testService,
                                 IDhlRegistrationService dhlRegistrationService,
                                 IMaintenanceService maintenanceService)
        {
            this.patientService = patientService;
            this.otherService = otherService;
            this.testService = testService;
            this.dhlRegistrationService = dhlRegistrationService;
            this.maintenanceService = maintenanceService;
        }


        [HttpGet("GetPatients")]
        public ActionResult GetPatients()
        {
            var responseModel = new PatientResponseModel();

            responseModel.PatientDetails = GetPatientDtos();
            responseModel.TestGroups = testService.GetTestGroups().ToList();
            responseModel.TestTitles = testService.GetTestTitles().ToList();
            responseModel.Initials = otherService.GetInitials().ToList();
            responseModel.Genders = otherService.GetGenders().ToList();
            responseModel.RegistrationTypes = otherService.GetReferredByTypes().ToList();
            responseModel.HdlRegistrations = dhlRegistrationService.GetDhlRegistrations().HdlRegistrations;
            responseModel.FieldOptions = maintenanceService.GetFieldOptions().FieldOptions;

            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No patients found", "Please register a patient")));
            }

        }


        [HttpPost("NewPatient")]
        public ActionResult InsertNewPatient(PatientDto patientRequestModel)
        {
            if (patientRequestModel != null)
            {
                try
                {
                    if (patientRequestModel.Patient != null)
                    {
                        patientService.InsertPatient(patientRequestModel.Patient);
                    }
                    else
                    {
                        return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Patient details are missing")));
                    }

                    //Insert the selected tests
                    if (patientRequestModel.SelectedTestTitles != null && patientRequestModel.SelectedTestTitles.Count > 0)
                    {
                        foreach (var title in patientRequestModel.SelectedTestTitles)
                        {

                            var otherTests = testService.GetOtherTests().Where(otherTest => otherTest.TestGroupId == title.GroupId && otherTest.TestTitleId == title.Id).ToList();
                            if (otherTests != null && otherTests.Count > 0)
                            {
                                foreach (var test in otherTests)
                                {
                                    testService.InsertTestResult(new TestResults().GetTestResult(test, patientRequestModel.Patient));
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while registering new patient")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient details")));
            }
        }


        [HttpPut("UpdatePatient")]
        public ActionResult UpdatePatient(PatientDto patientRequestModel)
        {
            if (patientRequestModel != null)
            {
                try
                {
                    if (patientRequestModel.Patient != null)
                    {
                        patientService.UpdatePatient(patientRequestModel.Patient);
                    }
                    else
                    {
                        return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Patient details are missing")));
                    }

                    //Update the test results
                    var patientTestResults = testService.GetTestResults().Where(x => x.PatientId == patientRequestModel.Patient.Id).ToList();

                    if (patientRequestModel.SelectedTestTitles != null && patientRequestModel.SelectedTestTitles.Count > 0)
                    {
                        //Add the tests with if the title is not already present
                        foreach (var title in patientRequestModel.SelectedTestTitles)
                        {
                            var testResults = patientTestResults.FindAll(x => x.TitleId == title.Id);
                            if (testResults != null && testResults.Count == 0)
                            {
                                var otherTests = testService.GetOtherTests().Where(otherTest => otherTest.TestGroupId == title.GroupId && otherTest.TestTitleId == title.Id).ToList();
                                if (otherTests != null && otherTests.Count > 0)
                                {
                                    foreach (var test in otherTests)
                                    {
                                        testService.InsertTestResult(new TestResults().GetTestResult(test, patientRequestModel.Patient));
                                    }
                                }
                            }
                        }

                        //Delete the remaining test results 
                        foreach (var testResult in patientTestResults)
                        {
                            var testTitle = patientRequestModel.SelectedTestTitles.FindAll(x => x.Id == testResult.TitleId);

                            if (testTitle != null && testTitle.Count == 0)
                            {
                                testService.DeleteTestResult(testResult.Id);
                            }
                        }
                    }
                    else
                    {
                        //Delete all the test results for this patient
                        if (patientTestResults != null && patientTestResults.Count > 0)
                        {
                            foreach (var testResult in patientTestResults)
                            {
                                testService.DeleteTestResult(testResult.Id);
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the test group")));
                }


                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient details")));
            }
        }

        [HttpDelete("DeletePatient")]
        public ActionResult DeletePatient(int id)
        {
            if (id > 0)
            {
                try
                {
                    //First delete all the test results of the patient and then delete the patient
                    var testResults = testService.GetTestResults().Where(x => x.PatientId == id).ToList();
                    foreach (var testResult in testResults)
                    {
                        testService.DeleteTestResult(testResult.Id);
                    }


                    patientService.DeletePatient(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the patient")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient id")));
            }
        }

        [HttpGet("GetPatientsWithTests")]
        public ActionResult GetPatientsWithTests()
        {
            var patients = patientService.GetAllPatients().ToList();
            var formulas = testService.GetFormulas().ToList();
            foreach (var patient in patients)
            {
                patient.GenderNavigation = otherService.GetGenders().ToList()?.Find(x => x.Id == patient.Gender);
                patient.Initial = otherService.GetInitials().ToList()?.Find(x => x.Id == patient.InitialId);
                patient.PatientFullName = $"{patient.Initial.Initial} {patient.FirstName}";
                if (!string.IsNullOrEmpty(patient.MiddleName))
                {
                    patient.PatientFullName += $" {patient.MiddleName}";
                }
                if (!string.IsNullOrEmpty(patient.LastName))
                {
                    patient.PatientFullName += $" {patient.LastName}";
                }

                patient.CivilStatusNavigation = maintenanceService.GetFieldOptions().FieldOptions?.Find(x => x.Id == patient.CivilStatus);
                if (patient.ReferredBy.HasValue)
                {
                    patient.ReferredByNavigation = dhlRegistrationService.GetDhlRegistrations().HdlRegistrations?.Find(x => x.Id == patient.ReferredBy);
                    if(patient.ReferredByNavigation != null)
                    {
                        if (patient.ReferredByNavigation.RegistrationTypeId == 1)
                        {
                            patient.ReferredByFullName = $"Dr. {patient.ReferredByNavigation.Name}";
                        }
                        else
                        {
                            patient.ReferredByFullName = patient.ReferredByNavigation.Name;
                        }
                    }
                    
                }
            }

            var responseModel = new TestResultsResponseModel();
            responseModel.Patients = patients;
            responseModel.Signatures = maintenanceService.GetSignatures().Signatures;

            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No patients found", "Please register a patient")));
            }
        }

        [HttpPost("GetPatientWithTests")]
        public ActionResult GetPatientWithTests(PatientTestResponseModel responseModel)
        {
            var patient = patientService.GetAllPatients().ToList().Find(x => x.Id == responseModel.PatientId);
            if (patient != null)
            {
                var formulas = testService.GetFormulas().ToList();
                patient.TestResults = testService.GetTestResultsFromView().Where(x => x.PatientId == patient.Id).ToList();
                
                foreach (var testResult in patient.TestResults)
                {
                    var formula = formulas.Find(x => x.TestId == testResult.OtherTestId);
                    testResult.Formula = formula;
                    if (patient.Gender.HasValue && patient.Gender == 2 && patient.AgeInYears > 13) //Female
                    {
                        testResult.NormalRange = GetNormalRange(testResult.OtherTestId)?.ValFemale;
                    }
                    else if (patient.AgeInYears < 3) //Neonatal
                    {
                        testResult.NormalRange = GetNormalRange(testResult.OtherTestId)?.ValNoenatal;
                    }
                    else if (patient.AgeInYears < 14) //Children
                    {
                        testResult.NormalRange = GetNormalRange(testResult.OtherTestId)?.ValChild;
                    }
                    testResult.TestGroup = testService.GetTestGroups().ToList()?.Find(x => x.Id == testResult.GroupId);
                    testResult.TestGroupOrderId = (int)(testResult.TestGroup?.OrderNo);
                    testResult.TestTitle = testService.GetTestTitles().ToList()?.Find(x => x.Id == testResult.TitleId);
                    testResult.TestTitleOrderId = (int)(testResult.TestTitle?.OrderBy);
                    testResult.OtherTest = testService.GetOtherTests().ToList()?.Find(x => x.Id == testResult.OtherTestId);
                    testResult.OtherTestOrderId = (int)(testResult.OtherTest?.OrderBy);
                }


                responseModel.TestResults = patient.TestResults;

                if (responseModel != null)
                {
                    return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No patients found", "Please register a patient")));
                }
            }
            else
            {
                throw new Exception("Patient does not exist");
            }
            
        }

        OtherTests GetNormalRange(int otherTestId)
        {
            return testService.GetOtherTests().ToList()?.Find(x => x.Id == otherTestId);
        }

        [HttpGet("GetPatientsWithBill")]
        public ActionResult GetPatientWithBill()
        {
            var patients = GetPatientDtosWithBill();

            var resposeModel = new BillResponseModel { Patients = patients };
            if (resposeModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No patients found", "Please register a patient")));
            }
        }

        [HttpPut("UpdatePatientTestResults")]
        public ActionResult UpdatePatientTestResults(Patient patient)
        {
            if (patient != null)
            {
                try
                {
                    patientService.UpdatePatientTestResults(patient);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the patients test results")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient test results details")));
            }
        }

        [HttpPost("GetPatientWithTestsWithRateListCharges")]
        public ActionResult GetPatientWithTestsWithRateListCharges(PatientWithRateListChargesRequest requestModel)
        {
            var resposeModel = patientService.GetPatientWithRateListCharges(requestModel);

            if (resposeModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No patients found", "Please register a patient")));
            }
        }

        [HttpPost("GetSpecializedLabSamples")]
        public ActionResult GetSpecializedLabSamples(SpecializedLabSampleDto requestModel)
        {
            var resposeModel = patientService.GetSpecializedLabSamples(requestModel);

            if (resposeModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No lab samples found", "Something went wrong!")));
            }
        }

        [HttpPut("UpdateSpecializedLabSamples")]
        public ActionResult UpdateSpecializedLabSamples(SpecializedLabSampleDto requestModel)
        {
            if (requestModel != null)
            {
                try
                {
                    patientService.UpdateSpecializedLabSamples(requestModel);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the lab samples")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please select proper lab samples")));
            }
        }

        [HttpPost("GetSpecializedLabReport")]
        public ActionResult GetSpecializedLabReport(SpecializedLabReportResponseModel requestModel)
        {
            try
            {
                var resposeModel = patientService.GetSpecializedLabReport(requestModel);

                if (resposeModel != null)
                {
                    return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No lab samples found", "Something went wrong!")));
                }
            }
            catch (Exception e)
            {
                Program.Logger.Error(e);
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", e.Message)));
            }

        }

        [HttpPost("GetHdlReferringReport")]
        public ActionResult GetHdlReferringReport(HdlReferringCutReportResponseModel requestModel)
        {
            try
            {
                var resposeModel = patientService.GetHdlReferringReport(requestModel);

                if (resposeModel != null)
                {
                    return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No test titles found", "Something went wrong!")));
                }
            }
            catch (Exception e)
            {
                Program.Logger.Error(e);
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", e.Message)));
            }

        }

        #region Private methods
        private List<PatientDto> GetPatientDtos()
        {
            var dtos = new List<PatientDto>();
            var patients = patientService.GetAllPatients().ToList();
            foreach (var patient in patients)
            {
                var dto = new PatientDto();
                dto.Patient = patient;

                var bill = patientService.GetPatientBill()?.ToList()?.Find(x => x.PatientId == patient.Id);
                dto.Patient.Bill = bill;

                dto.SelectedTestTitles = new List<TestTitles>();
                var testResults = testService.GetTestResults().Where(x => x.PatientId == patient.Id).ToList();
                if (testResults != null && testResults.Count > 0)
                {
                    foreach (var testResult in testResults)
                    {
                        var testTitle = testService.GetTestTitle(testResult.TitleId);
                        if (!dto.SelectedTestTitles.Contains(testTitle))
                        {
                            dto.SelectedTestTitles.Add(testTitle);
                        }
                    }
                }

                dtos.Add(dto);
            }

            return dtos;
        }

        private List<Patient> GetPatientDtosWithBill()
        {
            var patients = patientService.GetAllPatients().ToList();
            foreach (var patient in patients)
            {
                patient.TestResults = testService.GetTestResultsFromView().Where(x => x.PatientId == patient.Id).ToList();
                patient.GenderNavigation = otherService.GetGenders().ToList()?.Find(x => x.Id == patient.Gender);
                patient.CivilStatusNavigation = maintenanceService.GetFieldOptions().FieldOptions?.Find(x => x.Id == patient.CivilStatus);
                if (patient.ReferredBy.HasValue)
                {
                    patient.ReferredByNavigation = dhlRegistrationService.GetDhlRegistrations().HdlRegistrations?.Find(x => x.Id == patient.ReferredBy);
                }
                var testResults = testService.GetTestResults().Where(x => x.PatientId == patient.Id).ToList();
                patient.TestTitles = new List<TestTitles>();
                if (testResults != null && testResults.Count > 0)
                {
                    foreach (var testResult in testResults)
                    {
                        var testTitle = testService.GetTestTitle(testResult.TitleId);
                        if (!patient.TestTitles.Contains(testTitle))
                        {
                            testTitle.Group = testService.GetTestGroup(testTitle.GroupId);
                            patient.TestTitles.Add(testTitle);
                        }
                    }
                }

                var bill = patientService.GetPatientBill()?.ToList()?.Find(x => x.PatientId == patient.Id);
                patient.Bill = bill;

            }

            return patients;
        }


        #endregion
    }
}
