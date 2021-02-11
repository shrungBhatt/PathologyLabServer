using OPSMBackend.DataEntities;
using OPSMBackend.Models.Dto;
using OPSMBackend.Models.Request;
using OPSMBackend.Models.Response;
using OPSMBackend.Repositories;
using OPSMBackend.Services.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using PatientEntity = OPSMBackend.DataEntities.Patient;

namespace OPSMBackend.Services.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<PatientEntity> patientsRepository;
        private readonly IRepository<PatientBill> patientBillRepository;
        private readonly IRepository<PatientCounts> patientCountsRepository;
        private readonly IRepository<TestResults> testResultsRepository;
        private readonly IRepository<TestTitles> testTitlesRepository;
        private readonly IRepository<TestGroups> testGroupsRepository;
        private readonly IRepository<HdlRegistration> hdlRegisrationRepository;
        private readonly IRepository<MonthlyRateList> monthlyRateListRepository;
        private readonly IRepository<SpecializedLabSamples> specializedLabSamplesRepository;
        private readonly IRepository<DataEntities.SpecializedLabRateList> specializedLabRateListRepository;
        private readonly IRepository<ReferringRateList> referringRateListRepository;
        private readonly IUtil util;

        public PatientService(IRepository<PatientEntity> patientsRepository,
            IRepository<PatientBill> patientBillRepository,
            IRepository<PatientCounts> patientCountsRepository,
            IRepository<TestResults> testResultsRepository,
            IRepository<HdlRegistration> hdlRegisrationRepository,
            IRepository<TestTitles> testTitlesRepository,
            IRepository<TestGroups> testGroupsRepository,
            IRepository<MonthlyRateList> monthlyRateListRepository,
            IRepository<SpecializedLabSamples> specializedLabSamplesRepository,
            IRepository<DataEntities.SpecializedLabRateList> specializedLabRateListRepository,
            IRepository<ReferringRateList> referringRateListRepository,
            IUtil util)
        {
            this.patientsRepository = patientsRepository;
            this.patientBillRepository = patientBillRepository;
            this.patientCountsRepository = patientCountsRepository;
            this.testResultsRepository = testResultsRepository;
            this.hdlRegisrationRepository = hdlRegisrationRepository;
            this.testTitlesRepository = testTitlesRepository;
            this.testGroupsRepository = testGroupsRepository;
            this.monthlyRateListRepository = monthlyRateListRepository;
            this.specializedLabSamplesRepository = specializedLabSamplesRepository;
            this.specializedLabRateListRepository = specializedLabRateListRepository;
            this.referringRateListRepository = referringRateListRepository;
            this.util = util;
        }
        public void DeletePatient(int id)
        {
            patientsRepository.Delete(patientsRepository.Get(id));
        }

        public void DeletePatientBill(int id)
        {
            patientBillRepository.Delete(patientBillRepository.Get(id));
        }

        public IEnumerable<DataEntities.Patient> GetAllPatients()
        {
            return patientsRepository.GetAll().ToList();
        }

        public IEnumerable<PatientBill> GetPatientBill()
        {
            return patientBillRepository.GetAll().ToList();
        }

        public void InsertPatient(DataEntities.Patient patient)
        {
            patient.PatientCode = GetPatientCode();
            patientsRepository.Insert(patient);
        }

        string GetPatientCode()
        {
            var currentDate = DateTime.Today;
            var month = currentDate.Month;
            var year = currentDate.Year;

            PatientCounts patientCountModel;
            var patientCounts = patientCountsRepository.GetAll().ToList();
            if (patientCounts != null && patientCounts.Count > 0)
            {
                var currentYear = patientCounts.FindAll(x => x.YearName == year);
                if (currentYear != null)
                {
                    var currentMonth = currentYear.Find(x => x.MonthName == month);
                    if (currentMonth == null)
                    {
                        //Add a new month and continue
                        patientCountModel = new PatientCounts();
                        patientCountModel.MonthName = month;
                        patientCountModel.YearName = year;
                        patientCountModel.PatientCount = 0;
                        patientCountsRepository.Insert(patientCountModel);
                    }
                    else
                    {
                        patientCountModel = currentMonth;
                    }
                }
                else
                {
                    //Add a new month and continue
                    patientCountModel = new PatientCounts();
                    patientCountModel.MonthName = month;
                    patientCountModel.YearName = year;
                    patientCountModel.PatientCount = 0;
                    patientCountsRepository.Insert(patientCountModel);
                }
            }
            else
            {
                //Add a new month and continue
                patientCountModel = new PatientCounts();
                patientCountModel.MonthName = month;
                patientCountModel.YearName = year;
                patientCountModel.PatientCount = 0;
                patientCountsRepository.Insert(patientCountModel);
            }

            patientCountModel.PatientCount += 1;
            var code = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(patientCountModel.MonthName).ToUpper()}-OP-{patientCountModel.PatientCount.ToString("000")}";

            patientCountsRepository.Update(patientCountModel);

            return code;
        }

        public void InsertPatientBill(PatientBill patientBill)
        {
            patientBillRepository.Insert(patientBill);
        }

        public void UpdatePatient(DataEntities.Patient patient)
        {
            var patientFromDb = patientsRepository.Get(patient.Id);
            if (patientFromDb != null)
            {
                util.CopyProperties(patient, patientFromDb);
                patientsRepository.Update(patientFromDb);
            }
            else
            {
                throw new Exception("This patient that you are trying to update does not exist");
            }

            patientsRepository.Update(patient);
        }

        public void UpdatePatientBill(PatientBill patientBill)
        {
            patientBillRepository.Update(patientBill);
        }

        public void UpdatePatientTestResults(PatientEntity patient)
        {
            if (patient != null)
            {
                var testResults = testResultsRepository.GetAll().ToList().FindAll(x => x.PatientId == patient.Id);
                if (testResults != null && testResults.Count == patient.TestResults.Count)
                {
                    foreach (var testResultView in patient.TestResults)
                    {
                        var testResult = testResults.Find(x => x.Id == testResultView.TestResultId);
                        if (testResult != null)
                        {
                            testResult.TestResult = testResultView.TestResult;
                            testResult.LargeTestResult = testResultView.LargeTestResult;
                            testResultsRepository.Update(testResult);
                        }
                        else
                        {
                            Program.Logger.Warn("This test result does not exist");
                        }
                    }

                    var patientFromDb = patientsRepository.Get(patient.Id);
                    patientFromDb.IsFinished = true;
                    patientsRepository.Update(patientFromDb);
                }
                else
                {
                    throw new Exception("The patient test records do not match");
                }
            }
            else
            {
                throw new Exception("This patient does not exist");
            }
        }

        public PatientWithRateListChargesRequest GetPatientWithRateListCharges(PatientWithRateListChargesRequest model)
        {
            var response = model;

            if (model.FromDate == null || model.ToDate == null)
            {
                throw new Exception("Please enter valid from and to dates");
            }

            var patients = patientsRepository.GetAll().Where(x => x.RegistrationDate >= model.FromDate && x.RegistrationDate <= model.ToDate).ToList();
            if (patients != null && patients.Count > 0)
            {
                var patientsOfHdls = patients.Where(x => x.ReferredBy == model.HdlId)?.ToList();
                if (patientsOfHdls != null && patientsOfHdls.Count > 0)
                {
                    model.Patients = new List<Models.Dto.PatientWithRateListTestCharges>();
                    foreach (var patientOfHdl in patientsOfHdls)
                    {
                        var testResults = testResultsRepository.GetAll().Where(x => x.PatientId == patientOfHdl.Id).ToList();
                        var hdl = hdlRegisrationRepository.Get((int)patientOfHdl.ReferredBy);
                        var rates = monthlyRateListRepository.GetAll()?.Where(x => x.HdlId == hdl.Id)?.ToList();
                        patientOfHdl.TestTitles = new List<TestTitles>();
                        if (testResults != null && testResults.Count > 0)
                        {
                            foreach (var testResult in testResults)
                            {
                                var testTitle = testTitlesRepository.Get(testResult.TitleId);
                                if (!patientOfHdl.TestTitles.Contains(testTitle))
                                {
                                    var rate = rates.Find(x => x.TestTitleId == testTitle.Id);
                                    if (rate != null)
                                    {
                                        testTitle.Charges = (int)(rate.Charges);
                                    }
                                    testTitle.Group = testGroupsRepository.Get(testTitle.GroupId);
                                    patientOfHdl.TestTitles.Add(testTitle);
                                }
                            }
                        }

                        var patientModel = new Models.Dto.PatientWithRateListTestCharges
                        {
                            PatientCode = patientOfHdl.PatientCode,
                            PatientId = patientOfHdl.Id,
                            PatientName = $"{patientOfHdl.FirstName} {patientOfHdl.LastName}"
                        };
                        patientModel.TestTitles.AddRange(patientOfHdl.TestTitles);

                        model.Patients.Add(patientModel);
                    }
                }
            }

            return response;
        }

        public Models.Dto.SpecializedLabSampleDto GetSpecializedLabSamples(Models.Dto.SpecializedLabSampleDto specializedLabSampleDto)
        {
            var labs = hdlRegisrationRepository.GetAll()?.ToList()?.FindAll(x => x.RegistrationTypeId == 3);
            var savedSamples = specializedLabSamplesRepository.GetAll()?.ToList()?.FindAll(x => x.PatientId == specializedLabSampleDto.PatientId);
            if (savedSamples != null)
            {
                foreach (var sample in savedSamples)
                {
                    sample.TestName = testTitlesRepository.Get(sample.TestTitleId)?.Name;
                    sample.LabName = hdlRegisrationRepository.Get(sample.LabId)?.Name;
                }
            }

            return new Models.Dto.SpecializedLabSampleDto { PatientId = specializedLabSampleDto.PatientId, SentSamples = savedSamples, SpecializedLabs = labs };
        }

        public void UpdateSpecializedLabSamples(Models.Dto.SpecializedLabSampleDto specializedLabSampleDto)
        {
            if (specializedLabSampleDto != null)
            {
                var existingSamples = specializedLabSamplesRepository.GetAll()?.Where(x => x.PatientId == specializedLabSampleDto.PatientId)?.ToList();
                if (existingSamples != null && existingSamples.Count > 0)
                {
                    foreach (var sample in existingSamples)
                    {
                        specializedLabSamplesRepository.Delete(sample);
                    }
                }

                if (specializedLabSampleDto.SentSamples != null)
                {
                    foreach (var sample in specializedLabSampleDto.SentSamples)
                    {
                        specializedLabSamplesRepository.Insert(sample);
                    }
                }
            }
        }

        public SpecializedLabReportResponseModel GetSpecializedLabReport(SpecializedLabReportResponseModel model)
        {
            if (model.FromDate == null || model.ToDate == null)
            {
                throw new Exception("Please enter valid date range");
            }

            var patients = patientsRepository.GetAll().Where(x =>
            {
                if ((x.RegistrationDate >= model.FromDate) && (x.RegistrationDate <= model.ToDate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            })?.ToList();

            var responseModel = new SpecializedLabReportResponseModel();
            responseModel.Labs = hdlRegisrationRepository.GetAll()?.ToList()?.FindAll(x => x.RegistrationTypeId == 3);
            if (patients != null && patients.Count > 0)
            {
                responseModel.FromDate = model.FromDate;
                responseModel.ToDate = model.ToDate;
                foreach (var patient in patients)
                {
                    var samples = specializedLabSamplesRepository.GetAll()?.Where(x => x.PatientId == patient.Id)?.ToList();
                    if (samples != null)
                    {
                        foreach (var sample in samples)
                        {
                            var labSample = new SpecializedLabModelDto();
                            var testTitle = testTitlesRepository.Get(sample.TestTitleId);
                            var lab = hdlRegisrationRepository.Get(sample.LabId);
                            var _patient = patient;
                            var rateList = specializedLabRateListRepository.GetAll()?.ToList()?.Find(x => x.HdlId == lab.Id && x.TestTitleId == testTitle.Id);

                            labSample.TestTitleId = testTitle.Id;
                            labSample.TestGroupId = testTitle.GroupId;
                            labSample.TestName = testTitle.Name;
                            labSample.RegistrationDate = patient.RegistrationDate;
                            labSample.PatientName = patient.FirstName + " " + patient.LastName;
                            labSample.PatientCode = patient.PatientCode;
                            labSample.PatientId = patient.Id;
                            labSample.OutGoingLabName = lab.Name;
                            labSample.OutGoingLabId = lab.Id;
                            labSample.OriginalCharge = testTitle.Charges;
                            labSample.LabCharge = (float)rateList.Charges;

                            responseModel.SpecializedLabsSamples.Add(labSample);
                        }
                    }
                }

            }
            return responseModel;
        }

        public HdlReferringCutReportResponseModel GetHdlReferringReport(HdlReferringCutReportResponseModel model)
        {
            if (model.FromDate == null || model.ToDate == null)
            {
                throw new Exception("Please enter valid date range");
            }

            var patients = patientsRepository.GetAll().Where(x =>
            {
                if ((x.RegistrationDate >= model.FromDate) && (x.RegistrationDate <= model.ToDate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            })?.ToList();

            var responseModel = new HdlReferringCutReportResponseModel();
            responseModel.Hdls = hdlRegisrationRepository.GetAll()?.ToList()?.FindAll(x => x.RegistrationCategoryId == 2 && x.RegistrationTypeId != 3);
            if (patients != null && patients.Count > 0)
            {
                responseModel.FromDate = model.FromDate;
                responseModel.ToDate = model.ToDate;
                foreach (var patient in patients)
                {
                    var testResults = testResultsRepository.GetAll()?.Where(x => x.PatientId == patient.Id)?.ToList();
                    if (testResults != null)
                    {
                        List<TestTitles> selectedTests = new List<TestTitles>();
                        if (testResults.Count > 0)
                        {
                            foreach (var testResult in testResults)
                            {
                                var testTitle = testTitlesRepository.Get(testResult.TitleId);
                                if (!selectedTests.Contains(testTitle))
                                {
                                    selectedTests.Add(testTitle);
                                }
                            }
                        }

                        foreach (var test in selectedTests)
                        {
                            var labSample = new HdlReferringCutModelDto();
                            var testTitle = testTitlesRepository.Get(test.Id);
                            var lab = hdlRegisrationRepository.Get((int)patient.ReferredBy);
                            var _patient = patient;
                            var rateList = referringRateListRepository.GetAll()?.ToList()?.Find(x => x.HdlId == lab.Id && x.TestTitleId == testTitle.Id);

                            labSample.TestTitleId = testTitle.Id;
                            labSample.TestGroupId = testTitle.GroupId;
                            labSample.TestName = testTitle.Name;
                            labSample.RegistrationDate = patient.RegistrationDate;
                            labSample.PatientName = patient.FirstName + " " + patient.LastName;
                            labSample.PatientCode = patient.PatientCode;
                            labSample.PatientId = patient.Id;
                            labSample.HdlName = lab.Name;
                            labSample.HdlId = lab.Id;
                            labSample.OriginalCharge = testTitle.Charges;
                            var amount = GetReferringAmount(rateList, testTitle, labSample.HdlName);
                            if (amount.HasValue)
                            {
                                labSample.ReferringCharge = (float)amount;
                            }
                            else
                            {
                                labSample.ReferringCharge = 0;
                            }

                            responseModel.HdlReferringCutModelDtos.Add(labSample);
                        }
                    }
                }

            }
            return responseModel;
        }

        float? GetReferringAmount(ReferringRateList rateList, TestTitles testTitle, string registrationName)
        {
            if(rateList == null)
            {
                return null;
            }
            if (rateList.ReferringAmount.HasValue && rateList.ReferringAmount != 0)
            {
                return (float?)rateList.ReferringAmount;
            }
            else if (rateList.ReferringPercentage.HasValue && rateList.ReferringPercentage != 0)
            {
                return (float?)(testTitle.Charges * rateList.ReferringPercentage) / 100;
            }
            else
            {
                return null;
            }
        }
    }
}
