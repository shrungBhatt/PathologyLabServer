using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using OPSMBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Tests
{
    public class TestsService : ITestsService
    {

        private IRepository<TestGroups> testGroupsRepository;
        private IRepository<TestTitles> testTitlesRepository;
        private IRepository<OtherTests> otherTestsRepository;
        private IRepository<TestResults> testResultsRepository;
        private IRepository<TestResultsView> testResultsViewRepository;
        private IRepository<Formulas> formulasRepository;

        public TestsService(IRepository<TestGroups> testGroupsRepository,
                            IRepository<TestTitles> testTitlesRepository,
                            IRepository<OtherTests> otherTestsRepository,
                            IRepository<TestResults> testResultsRepository,
                            IRepository<TestResultsView> testResultsViewRepository,
                            IRepository<Formulas> formulasRepository)
        {
            this.testGroupsRepository = testGroupsRepository;
            this.testTitlesRepository = testTitlesRepository;
            this.otherTestsRepository = otherTestsRepository;
            this.testResultsRepository = testResultsRepository;
            this.testResultsViewRepository = testResultsViewRepository;
            this.formulasRepository = formulasRepository;
        }

        public void AddTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model)
        {
            foreach(var test in model.SelectedTestTitles)
            {
                var testTitle = new TestTitles();
                testTitle.Charges = test.Charges;
                testTitle.FooterNote = test.FooterNote;
                testTitle.HeaderNote = test.HeaderNote;
                testTitle.GroupId = model.SelectedGroupId;
                testTitle.ModifiedDate = DateTime.Now;
                testTitle.Name = test.Name;
                testTitle.OrderBy = test.OrderBy;
                testTitle.ShowNormalRangeHeader = test.ShowNormalRangeHeader;
                testTitle.WordFormatResult = test.WordFormatResult;
                testTitle.ModifiedBy = model.ModifiedBy;

                testTitlesRepository.Insert(testTitle);

                var otherTests = otherTestsRepository.GetAll().Where(x => x.TestTitleId == test.Id).ToList();
                foreach(var otherTest in otherTests)
                {
                    var otherTestModel = new OtherTests();
                    otherTestModel.TestGroupId = testTitle.GroupId;
                    otherTestModel.TestTitleId = testTitle.Id;
                    otherTestModel.Name = otherTest.Name;
                    otherTestModel.OrderBy = otherTest.OrderBy;
                    otherTestModel.Unit = otherTest.Unit;
                    otherTestModel.DisplayInTestResult = otherTest.DisplayInTestResult;
                    otherTestModel.DisplayInBoldFontInReport = otherTest.DisplayInBoldFontInReport;
                    otherTestModel.DescriptiveResult = otherTest.DescriptiveResult;
                    otherTestModel.ValMale = otherTest.ValMale;
                    otherTestModel.ValFemale = otherTest.ValFemale;
                    otherTestModel.ValNoenatal = otherTest.ValNoenatal;
                    otherTestModel.ValChild = otherTest.ValChild;
                    otherTestModel.DefaultValue = otherTest.DefaultValue;
                    otherTestModel.Options = otherTest.Options;
                    otherTestModel.ModifiedDate = DateTime.Now;
                    otherTestModel.ModifiedBy = model.ModifiedBy;

                    otherTestsRepository.Insert(otherTestModel);
                }
            }
        }

        public void DeleteFormula(int id)
        {
            formulasRepository.Delete(formulasRepository.Get(id));
        }

        public void DeleteOtherTest(int id)
        {
            otherTestsRepository.Delete(otherTestsRepository.Get(id));
        }

        public void DeleteTestGroup(int id)
        {
            testGroupsRepository.Delete(testGroupsRepository.Get(id));
        }

        public void DeleteTestResult(int id)
        {
            testResultsRepository.Delete(testResultsRepository.Get(id));
        }

        public void DeleteTestTitle(int id)
        {
            testTitlesRepository.Delete(testTitlesRepository.Get(id));
        }

        public Formulas GetFormula(int id)
        {
            return formulasRepository.Get(id);
        }

        public IEnumerable<Formulas> GetFormulas()
        {
            return formulasRepository.GetAll().ToList();
        }

        public IEnumerable<OtherTests> GetOtherTests()
        {
            return otherTestsRepository.GetAll().ToList();
        }

        public OtherTests GetOtherTests(int id)
        {
            return otherTestsRepository.Get(id);
        }

        public TestGroups GetTestGroup(int id)
        {
            return testGroupsRepository.Get(id);
        }

        public IEnumerable<TestGroups> GetTestGroups()
        {
            return testGroupsRepository.GetAll().ToList();
        }

        public TestResults GetTestResult(int id)
        {
            return testResultsRepository.Get(id);
        }

        public IEnumerable<TestResults> GetTestResults()
        {
            return testResultsRepository.GetAll().ToList();
        }

        public IEnumerable<TestResultsView> GetTestResultsFromView()
        {

            var results = testResultsViewRepository.GetAll().ToList();

            if(results != null && results.Count > 0)
            {
                foreach(var result in results)
                {
                    if (string.IsNullOrEmpty(result.TestResult))
                    {
                        var otherTest = otherTestsRepository.Get(result.OtherTestId);
                        if(otherTest != null && !string.IsNullOrEmpty(otherTest.DefaultValue))
                        {
                            result.TestResult = otherTest.DefaultValue;
                        }
                    }
                }
            }

            return results;
        }

        public TestTitles GetTestTitle(int id)
        {
            return testTitlesRepository.Get(id);
        }

        public IEnumerable<TestTitles> GetTestTitles()
        {
            return testTitlesRepository.GetAll().ToList();
        }

        public AddTestTitlesInGoupResponseModel GetTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model)
        {
            var testGroups = testGroupsRepository.GetAll().ToList();
            var testTitles = testTitlesRepository.GetAll().ToList();

            foreach(var testTitle in testTitles)
            {
                var exists = testTitles.Find(x => x.GroupId == model.SelectedGroupId) != null;
                if(testTitle.GroupId != model.SelectedGroupId && !exists)
                {
                    model.TestTitles.Add(testTitle);
                }
            }

            model.TestTitles = testTitles;
            return model;
        }

        public void InsertFormula(Formulas formula)
        {
            formulasRepository.Insert(formula);
        }

        public void InsertOtherTest(OtherTests otherTests)
        {
            otherTestsRepository.Insert(otherTests);
        }

        public void InsertTestGroup(TestGroups testGroup)
        {
            testGroupsRepository.Insert(testGroup);
        }

        public void InsertTestResult(TestResults testResult)
        {
            testResultsRepository.Insert(testResult);
        }

        public void InsertTestTitle(TestTitles testTitle)
        {
            testTitlesRepository.Insert(testTitle);
        }

        public void UpdateFormula(Formulas formula)
        {
            formulasRepository.Update(formula);
        }

        public void UpdateOtherTest(OtherTests otherTests)
        {
            otherTestsRepository.Update(otherTests);
        }

        public void UpdateTestGroup(TestGroups testGroup)
        {
            testGroupsRepository.Update(testGroup);
        }

        public void UpdateTestResult(TestResults testResult)
        {
            testResultsRepository.Update(testResult);
        }

        public void UpdateTestTitle(TestTitles testTitle)
        {
            testTitlesRepository.Update(testTitle);
        }
    }
}
