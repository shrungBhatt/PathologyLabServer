using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Tests
{
    public interface ITestsService
    {
        IEnumerable<TestGroups> GetTestGroups();
        IEnumerable<TestTitles> GetTestTitles();
        IEnumerable<OtherTests> GetOtherTests();
        IEnumerable<TestResults> GetTestResults();
        IEnumerable<Formulas> GetFormulas();
        IEnumerable<TestResultsView> GetTestResultsFromView();
        TestGroups GetTestGroup(int id);
        TestTitles GetTestTitle(int id);
        OtherTests GetOtherTests(int id);
        TestResults GetTestResult(int id);
        Formulas GetFormula(int id);
        void InsertTestGroup(TestGroups testGroup);
        void UpdateTestGroup(TestGroups testGroup);
        void DeleteTestGroup(int id);
        void InsertTestTitle(TestTitles testTitle);
        void UpdateTestTitle(TestTitles testTitle);
        void DeleteTestTitle(int id);
        void InsertOtherTest(OtherTests otherTests);
        void UpdateOtherTest(OtherTests otherTests);
        void DeleteOtherTest(int id);
        void InsertTestResult(TestResults testResult);
        void UpdateTestResult(TestResults testResult);
        void DeleteTestResult(int id);
        void InsertFormula(Formulas formula);
        void UpdateFormula(Formulas formula);
        void DeleteFormula(int id);
        AddTestTitlesInGoupResponseModel GetTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model);
        void AddTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model);

    }
}
