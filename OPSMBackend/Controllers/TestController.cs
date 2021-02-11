using Microsoft.AspNetCore.Mvc;
using OPSMBackend.DataEntities;
using OPSMBackend.Models;
using OPSMBackend.Models.Dto;
using OPSMBackend.Models.Response;
using OPSMBackend.Services.Logger;
using OPSMBackend.Services.Tests;
using OPSMBackend.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {

        private ITestsService testsService;
        private readonly ILog Log;

        public TestController(ITestsService testsService, ILog log)
        {
            this.testsService = testsService;
            Log = log;
        }

        //API for getting all the test groups
        [HttpGet("TestGroups")]
        public ActionResult GetTestGroups()
        {
            var testResponseModel = new TestResponseModel();

            var testGroups = testsService.GetTestGroups().ToList();
            List<TestGroupDto> testGroupDtos = new List<TestGroupDto>();

            testGroups.ForEach(testGroup => testGroupDtos.Add((new TestGroupDto()).GetTestGroupDto(testGroup)));

            testResponseModel.TestGroups = testGroupDtos;

            if (testGroupDtos != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, testResponseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No test groups found", "Please add a test group")));
            }

        }

        //API for getting all the test titles
        [HttpGet("TestTitles")]
        public ActionResult GetTestTitles()
        {
            var testResponseModel = new TestResponseModel();

            var testTitles = testsService.GetTestTitles().ToList();
            List<TestTitleDto> testTitlesDtos = new List<TestTitleDto>();

            testTitles.ForEach(testTitle => testTitlesDtos.Add((new TestTitleDto()).GetTestTitleDto(testTitle)));

            testResponseModel.TestTitles = testTitlesDtos;
            testResponseModel.Groups = GetTestGroupTypeDtos(testsService.GetTestGroups().ToList());

            if (testTitlesDtos != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, testResponseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No test title found", "Please add a test title")));
            }

        }

        //API for getting all the test titles by group id
        [HttpGet("TestTitles/GroupId")]
        public ActionResult GetTestTitles(int groupId)
        {
            var testResponseModel = new TestResponseModel();

            var testTitles = testsService.GetTestTitles().ToList();
            List<TestTitleDto> testTitlesDtos = new List<TestTitleDto>();

            testTitles.ForEach(testTitle =>
            {
                if (testTitle.GroupId == groupId)
                {
                    testTitlesDtos.Add((new TestTitleDto()).GetTestTitleDto(testTitle));
                }

            });

            testResponseModel.TestTitles = testTitlesDtos;


            if (testTitlesDtos != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, testResponseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No test title found", "Please add a test title")));
            }

        }

        //API for getting all the other tests
        [HttpGet("OtherTests")]
        public ActionResult GetOtherTests()
        {
            var testResponseModel = new TestResponseModel();

            var otherTests = testsService.GetOtherTests().ToList();
            List<OtherTestDto> otherTestDtos = new List<OtherTestDto>();

            otherTests.ForEach(otherTest => otherTestDtos.Add((new OtherTestDto()).GetOtherTestDto(otherTest)));

            testResponseModel.OtherTests = otherTestDtos;
            testResponseModel.Groups = GetTestGroupTypeDtos(testsService.GetTestGroups().ToList());
            testResponseModel.Titles = GetTestTitleTypeDtos(testsService.GetTestTitles().ToList());

            if (otherTestDtos != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, testResponseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No other test found", "Please add an other test")));
            }

        }

        [HttpGet("Formulas")]
        public ActionResult GetFormulas()
        {
            var formulasResponseModel = new FormulasResponseModel();

            formulasResponseModel.OtherTests = testsService.GetOtherTests().ToList();
            formulasResponseModel.Groups = testsService.GetTestGroups().ToList();
            formulasResponseModel.Titles = testsService.GetTestTitles().ToList();
            formulasResponseModel.Formulas = testsService.GetFormulas().ToList();

            if (formulasResponseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, formulasResponseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No formula found", "Please add a formula")));
            }
        }

        //API for inserting a test group
        [HttpPost("NewTestGroup")]
        public ActionResult AddNewTestGroup(TestGroupDto testGroupDto)
        {
            if (testGroupDto != null)
            {
                var testGroup = testGroupDto.GetTestGroup(testGroupDto);
                if (testGroup != null)
                {
                    try
                    {
                        testsService.InsertTestGroup(testGroup);
                    }
                    catch (Exception e)
                    {
                        Program.Logger.Error(e);
                        return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new group")));
                    }

                    return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Something went wrong.")));
                }
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper group details")));
            }
        }

        //API for inserting a test title
        [HttpPost("NewTestTitle")]
        public ActionResult AddNewTestTitle(TestTitleDto testTitleDto)
        {
            if (testTitleDto != null)
            {
                var testTitle = testTitleDto.GetTestTile(testTitleDto);
                if (testTitle != null)
                {
                    try
                    {
                        testsService.InsertTestTitle(testTitle);
                    }
                    catch (Exception e)
                    {
                        Program.Logger.Error(e);
                        return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new test title")));
                    }

                    return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Something went wrong.")));
                }
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test title details")));
            }
        }

        //API for inserting an other test
        [HttpPost("NewOtherTest")]
        public ActionResult AddNewOtherTest(OtherTestDto otherTestDto)
        {
            if (otherTestDto != null)
            {
                var otherTest = otherTestDto.GetOtherTest(otherTestDto);
                if (otherTest != null)
                {
                    try
                    {
                        testsService.InsertOtherTest(otherTest);
                    }
                    catch (Exception e)
                    {
                        Program.Logger.Error(e);
                        return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new other test")));
                    }

                    return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Something went wrong.")));
                }
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other test details")));
            }
        }

        [HttpPost("NewFormula")]
        public ActionResult AddNewFormula(Formulas formulas)
        {
            if (formulas != null)
            {
                try
                {
                    testsService.InsertFormula(formulas);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new formula")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));

            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper formula details")));
            }
        }

        //API for updating a test group
        [HttpPut("UpdateTestGroup")]
        public ActionResult UpdateTestGroup(TestGroupDto testGroupDto)
        {
            if (testGroupDto != null)
            {

                try
                {
                    var testGroupFromDb = testsService.GetTestGroup(testGroupDto.Id);
                    if (testGroupFromDb != null)
                    {
                        var convertedTestGroup = testGroupDto.GetTestGroup(testGroupFromDb, testGroupDto);
                        testsService.UpdateTestGroup(convertedTestGroup);
                    }
                    else
                    {
                        throw new Exception("The test group does not exist");
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
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test group details")));
            }
        }

        //API for updating a test title
        [HttpPut("UpdateTestTitle")]
        public ActionResult UpdateTestTitle(TestTitleDto testTitleDto)
        {
            if (testTitleDto != null)
            {

                try
                {
                    var testTitleFromDb = testsService.GetTestTitle(testTitleDto.Id);
                    if (testTitleFromDb != null)
                    {
                        var convertedTestTitle = testTitleDto.GetTestTitle(testTitleFromDb, testTitleDto);
                        testsService.UpdateTestTitle(convertedTestTitle);
                    }
                    else
                    {
                        throw new Exception("The test title does not exist");
                    }

                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the test title")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test title details")));
            }
        }

        //API for updating an other test
        [HttpPut("UpdateOtherTest")]
        public ActionResult UpdateOtherTest(OtherTestDto otherTestDto)
        {
            if (otherTestDto != null)
            {

                try
                {
                    var otherTestFromDb = testsService.GetOtherTests(otherTestDto.Id);
                    if (otherTestFromDb != null)
                    {
                        var convertedTestTitle = otherTestDto.GetOtherTest(otherTestFromDb, otherTestDto);
                        testsService.UpdateOtherTest(convertedTestTitle);
                    }
                    else
                    {
                        throw new Exception("Provided other test does not exist");
                    }

                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the other test")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other test details")));
            }
        }

        [HttpPut("UpdateFormula")]
        public ActionResult UpdateFormula(Formulas formula)
        {
            if (formula != null)
            {

                try
                {
                    var formulaFromDb = testsService.GetFormula(formula.Id);
                    formulaFromDb.Formula = formula.Formula;
                    formulaFromDb.ModifiedBy = formula.ModifiedBy;
                    formulaFromDb.ModifiedDate = formula.ModifiedDate;
                    formulaFromDb.TestId = formula.TestId;
                    formulaFromDb.TitleId = formula.TitleId;
                    formulaFromDb.GroupId = formula.GroupId;

                    testsService.UpdateFormula(formulaFromDb);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the formula")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other formula details")));
            }
        }

        //API for deleting a test group
        [HttpDelete("DeleteTestGroup")]
        public ActionResult DeleteTestGroup(int id)
        {
            if (id > 0)
            {
                try
                {
                    testsService.DeleteTestGroup(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the test group")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test group details")));
            }
        }

        //API for deleting a test title
        [HttpDelete("DeleteTestTitle")]
        public ActionResult DeleteTestTitle(int id)
        {
            if (id > 0)
            {
                try
                {
                    testsService.DeleteTestTitle(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the test title")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test title details")));
            }
        }


        //API for deleting an other test
        [HttpDelete("DeleteOtherTest")]
        public ActionResult DeleteOtherTest(int id)
        {
            if (id > 0)
            {
                try
                {
                    testsService.DeleteOtherTest(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the other test")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other test details")));
            }
        }

        [HttpDelete("DeleteFormula")]
        public ActionResult DeleteFormula(int id)
        {
            if (id > 0)
            {
                try
                {
                    testsService.DeleteFormula(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the formula")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other formula details")));
            }
        }

        [HttpPost("GetTestTitlesForOtherGroup")]
        public ActionResult GetTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel requestModel)
        {
            try
            {
                var resposeModel = testsService.GetTestTitlesForOtherGroup(requestModel);

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

        [HttpPost("AddTestTitlesForOtherGroup")]
        public ActionResult AddTestTitlesForOtherGroup(AddTestTitlesInGoupResponseModel model)
        {
            if (model != null)
            {
                try
                {
                    testsService.AddTestTitlesForOtherGroup(model);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", e.Message)));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));

            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper formula details")));
            }
        }

        private List<TestComboListTypeDto> GetTestGroupTypeDtos(List<TestGroups> testGroups)
        {
            var list = new List<TestComboListTypeDto>();
            testGroups.ForEach(group => list.Add(new TestComboListTypeDto().GetTestGroupDto(group)));

            return list;
        }

        private List<TestComboListTypeDto> GetTestTitleTypeDtos(List<TestTitles> testTitles)
        {
            var list = new List<TestComboListTypeDto>();
            testTitles.ForEach(group => list.Add(new TestComboListTypeDto().GetTestTitleDto(group)));

            return list;
        }
    }
}
