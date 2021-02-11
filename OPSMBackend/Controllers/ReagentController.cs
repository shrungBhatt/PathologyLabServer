using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OPSMBackend.DataEntities;
using OPSMBackend.Models;
using OPSMBackend.Models.Response;
using OPSMBackend.Services.Reagent;
using OPSMBackend.Services.Tests;

namespace OPSMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReagentController : BaseController
    {

        readonly IReagentService _reagentService;
        readonly ITestsService _testsService;

        public ReagentController(IReagentService reagentService, ITestsService testsService)
        {
            _reagentService = reagentService;
            _testsService = testsService;
        }


        [HttpGet("Reagents")]
        public ActionResult GetAllReagents()
        {
            var reagents = _reagentService.GetReagents().ToList();
            var responseModel = new ReagentResponseModel { Reagents = reagents };
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No reagents found", "Please add a reagent")));
            }
        }

        [HttpGet("TestReagentRelations")]
        public ActionResult GetAllTestReagentRelations()
        {
            var relations = _reagentService.GetTestReagentRelations().ToList();
            var reagents = _reagentService.GetReagents().ToList();
            var tests = _testsService.GetOtherTests().ToList();
            var responseModel = new ReagentResponseModel { TestReagentRelations = relations, Reagents = reagents, Tests = tests};
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No test reagent relation found", "Please add a new one")));
            }
        }

        //API for inserting a test group
        [HttpPost("NewReagent")]
        public ActionResult AddNewReagent(Reagents reagent)
        {
            if (reagent != null)
            {
                try
                {
                    _reagentService.InsertReagent(reagent);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new reagent")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper reagent details")));
            }
        }

        [HttpPost("NewTestReagentRelation")]
        public ActionResult AddNewTestReagentRelation(TestReagentRelation testReagentRelation)
        {
            if (testReagentRelation != null)
            {
                try
                {
                    _reagentService.InsertTestReagentRelation(testReagentRelation);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new test reagent relation")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test reagent relation details")));
            }
        }

        [HttpPut("UpdateReagent")]
        public ActionResult UpdateReagent(Reagents reagent)
        {
            if (reagent != null)
            {
                try
                {
                    _reagentService.UpdateReagent(reagent);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the reagent")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper reagent details")));
            }
        }

        [HttpPut("UpdateTestReagentRelation")]
        public ActionResult UpdateTestReagentRelation(TestReagentRelation testReagentRelation)
        {
            if (testReagentRelation != null)
            {
                try
                {
                    _reagentService.UpdateTestReagentRelation(testReagentRelation);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the test reagent relation")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test reagent relation details")));
            }
        }

        [HttpDelete("DeleteReagent")]
        public ActionResult DeleteReagent(int id)
        {
            if (id > 0)
            {
                try
                {
                    _reagentService.DeleteReagent(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the reagent")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper reagent details")));
            }
        }

        [HttpDelete("DeleteTestReagentRelation")]
        public ActionResult DeleteTestReagentRelation(int id)
        {
            if (id > 0)
            {
                try
                {
                    _reagentService.DeleteTestReagentRelation(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the test reagent relation")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper test reagent relation details")));
            }
        }

        #region Dealers
        [HttpGet("GetDealers")]
        public ActionResult GetDealers()
        {
            var responseModel = _reagentService.GetDealers();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add a dealer")));
            }
        }

        [HttpPost("NewDealer")]
        public ActionResult AddNewDealer(Dealers dealer)
        {
            if (dealer != null)
            {
                try
                {
                    _reagentService.InsertDealer(dealer);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new dealer")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper dealer details")));
            }
        }

        [HttpPut("UpdateDealer")]
        public ActionResult UpdateDealer(Dealers dealer)
        {
            if (dealer != null)
            {
                try
                {
                    _reagentService.UpdateDealer(dealer);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the dealer details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper dealer details")));
            }
        }

        [HttpDelete("DeleteDealer")]
        public ActionResult DeleteDealer(int id)
        {
            if (id > 0)
            {
                try
                {
                    _reagentService.DeleteDealer(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the dealer details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper dealer details")));
            }
        }
        #endregion

        #region Reagent entries
        [HttpGet("GetReagentEntries")]
        public ActionResult GetReagentEntries()
        {
            var responseModel = _reagentService.GetReagentEntries();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add a new reagent entry")));
            }
        }

        [HttpPost("NewReagentEntry")]
        public ActionResult AddNewReagentEntry(ReagentBillEntries reagentEntry)
        {
            if (reagentEntry != null)
            {
                try
                {
                    _reagentService.InsertReagentEntry(reagentEntry);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new reagent entry")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper reagent entry details")));
            }
        }

        [HttpPut("UpdateReagentEntry")]
        public ActionResult UpdateReagentEntry(ReagentBillEntries reagentEntry)
        {
            if (reagentEntry != null)
            {
                try
                {
                    _reagentService.UpdateReagentEntry(reagentEntry);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the reagent entry details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper reagent entry details")));
            }
        }

        [HttpDelete("DeleteReagentEntry")]
        public ActionResult DeleteReagentEntry(int id)
        {
            if (id > 0)
            {
                try
                {
                    _reagentService.DeleteReagentEntry(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the reagent entry details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper reagent entry details")));
            }
        }
        #endregion

    }
}
