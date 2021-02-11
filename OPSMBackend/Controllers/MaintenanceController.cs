using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OPSMBackend.DataEntities;
using OPSMBackend.Models;
using OPSMBackend.Models.Response;
using OPSMBackend.Services.DhlRegistration;
using OPSMBackend.Services.Maintenance;

namespace OPSMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : BaseController
    {

        private readonly IDhlRegistrationService dhlRegistrationService;
        private readonly IMaintenanceService maintenanceService;

        public MaintenanceController(IDhlRegistrationService dhlRegistrationService,
            IMaintenanceService maintenanceService)
        {
            this.dhlRegistrationService = dhlRegistrationService;
            this.maintenanceService = maintenanceService;
        }

        #region Doctor/Hospital/Lab Registration
        [HttpGet("GetDhlRegistrations")]
        public ActionResult GetAllReagents()
        {
            var responseModel = dhlRegistrationService.GetDhlRegistrations();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No registrations found", "Please add a reagent")));
            }
        }

        [HttpPost("NewDhlRegistration")]
        public ActionResult AddNewDhlRegistration(HdlRegistration hdlRegistration)
        {
            if (hdlRegistration != null)
            {
                try
                {
                    dhlRegistrationService.InsertDhlRegistration(hdlRegistration);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new registration")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper registration details")));
            }
        }

        [HttpPut("UpdateDhlRegistration")]
        public ActionResult UpdateDhlRegistration(HdlRegistration hdlRegistration)
        {
            if (hdlRegistration != null)
            {
                try
                {
                    dhlRegistrationService.UpdateDhlRegistration(hdlRegistration);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the registration")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper registration details")));
            }
        }

        [HttpDelete("DeleteDhlRegistration")]
        public ActionResult DeleteDhlRegistration(int id)
        {
            if (id > 0)
            {
                try
                {
                    dhlRegistrationService.DeleteDhlRegistration(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the registration")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper registration details")));
            }
        }
        #endregion

        #region Rate list
        [HttpGet("GetRateLists")]
        public ActionResult GetRateLists()
        {
            var responseModel = dhlRegistrationService.GetRateLists();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No registrations found", "Please add a reagent")));
            }
        }

        [HttpPost("NewRateList")]
        public ActionResult AddRateList(RateListModel rateListModel)
        {
            if (rateListModel != null)
            {
                try
                {
                    dhlRegistrationService.InsertRateList(rateListModel);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new rate list")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper registration details")));
            }
        }

        [HttpPut("UpdateRateList")]
        public ActionResult UpdateRateList(RateListModel rateListModel)
        {
            if (rateListModel != null)
            {
                try
                {
                    dhlRegistrationService.UpdateRateList(rateListModel);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the rate list")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper rate details")));
            }
        }

        [HttpDelete("DeleteRateList")]
        public ActionResult DeleteRateList(int id)
        {
            if (id > 0)
            {
                try
                {
                    dhlRegistrationService.DeleteRateList(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the rate list for this registration")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper registration details")));
            }
        }
        #endregion

        #region Inventory
        [HttpGet("GetInventoryList")]
        public ActionResult GetInventoryList()
        {
            var responseModel = maintenanceService.GetInventories();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No inventory found", "Please add an inventory item")));
            }
        }

        [HttpPost("NewInventory")]
        public ActionResult AddNewInventory(Inventories inventory)
        {
            if (inventory != null)
            {
                try
                {
                    maintenanceService.InsertInventory(inventory);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new inventory item")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper inventory details")));
            }
        }

        [HttpPut("UpdateInventory")]
        public ActionResult UpdateInventory(Inventories inventory)
        {
            if (inventory != null)
            {
                try
                {
                    maintenanceService.UpdateInventory(inventory);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the inventory")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper inventory details")));
            }
        }

        [HttpDelete("DeleteInventory")]
        public ActionResult DeleteInventory(int id)
        {
            if (id > 0)
            {
                try
                {
                    maintenanceService.DeleteInventory(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the inventory item")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper inventory details")));
            }
        }
        #endregion

        #region Employee
        [HttpGet("GetEmployee")]
        public ActionResult GetEmployee()
        {
            var responseModel = maintenanceService.GetEmployees();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No inventory found", "Please add an employee item")));
            }
        }

        [HttpPost("NewEmployee")]
        public ActionResult AddEmployee(Employees employee)
        {
            if (employee != null)
            {
                try
                {
                    maintenanceService.InsertEmployee(employee);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new employee")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper employee details")));
            }
        }

        [HttpPut("UpdateEmployee")]
        public ActionResult UpdateEmployee(Employees employee)
        {
            if (employee != null)
            {
                try
                {
                    maintenanceService.UpdateEmployee(employee);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the employee details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper employee details")));
            }
        }

        [HttpDelete("DeleteEmployee")]
        public ActionResult DeleteEmployee(int id)
        {
            if (id > 0)
            {
                try
                {
                    maintenanceService.DeleteEmployee(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the employee details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper employee details")));
            }
        }
        #endregion

        #region Salary
        [HttpGet("GetSalary")]
        public ActionResult GetSalary()
        {
            var responseModel = maintenanceService.GetSalaries();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an salary")));
            }
        }

        [HttpPost("NewSalary")]
        public ActionResult AddSalary(Salary salary)
        {
            if (salary != null)
            {
                try
                {
                    maintenanceService.InsertSalary(salary);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new salary")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper salary details")));
            }
        }

        [HttpPut("UpdateSalary")]
        public ActionResult UpdateSalary(Salary salary)
        {
            if (salary != null)
            {
                try
                {
                    maintenanceService.UpdateSalary(salary);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the salary details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper salary details")));
            }
        }

        [HttpDelete("DeleteSalary")]
        public ActionResult DeleteSalary(int id)
        {
            if (id > 0)
            {
                try
                {
                    maintenanceService.DeleteSalary(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the salary details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper salary details")));
            }
        }
        #endregion

        #region Signature
        [HttpGet("GetSignature")]
        public ActionResult GetSignature()
        {
            var responseModel = maintenanceService.GetSignatures();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an signature")));
            }
        }

        [HttpPost("NewSignature")]
        public ActionResult AddSignature(SignaturePrototypes signature)
        {
            if (signature != null)
            {
                try
                {
                    maintenanceService.InsertSignature(signature);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new signature")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper signature details")));
            }
        }

        [HttpPut("UpdateSignature")]
        public ActionResult UpdateSignature(SignaturePrototypes signature)
        {
            if (signature != null)
            {
                try
                {
                    maintenanceService.UpdateSignature(signature);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the signature details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper Signature details")));
            }
        }

        [HttpDelete("DeleteSignature")]
        public ActionResult DeleteSignature(int id)
        {
            if (id > 0)
            {
                try
                {
                    maintenanceService.DeleteSignature(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the signature details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper signature details")));
            }
        }
        #endregion

        #region Abbrevation & Interpretation
        [HttpGet("GetAbbrInterp")]
        public ActionResult GetAbbrInterp()
        {
            var responseModel = maintenanceService.GetAbbrevations();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an abbrevation")));
            }
        }

        [HttpPost("NewAbbrInterp")]
        public ActionResult AddAbbrInterp(Abbrevations abbrevations)
        {
            if (abbrevations != null)
            {
                try
                {
                    maintenanceService.InsertAbbrevation(abbrevations);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new abbrevation")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper abbrevation details")));
            }
        }

        [HttpPut("UpdateAbbrInterp")]
        public ActionResult UpdateAbbrInterp(Abbrevations abbrevation)
        {
            if (abbrevation != null)
            {
                try
                {
                    maintenanceService.UpdateAbbrevation(abbrevation);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the abbrevation details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper abbrevation details")));
            }
        }

        [HttpDelete("DeleteAbbrInterp")]
        public ActionResult DeleteAbbrInterp(int id)
        {
            if (id > 0)
            {
                try
                {
                    maintenanceService.DeleteAbbrevation(id);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the abbrevation details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper abbrevation details")));
            }
        }
        #endregion

        #region Field Options
        [HttpGet("GetFieldOptions")]
        public ActionResult GetFieldOptions()
        {
            var responseModel = maintenanceService.GetFieldOptions();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an field option")));
            }
        }

        [HttpPost("NewFieldOptions")]
        public ActionResult AddFieldOptions(FieldOptions fieldOption)
        {
            if (fieldOption != null)
            {
                try
                {
                    maintenanceService.InsertFieldOption(fieldOption);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new field option")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper field option details")));
            }
        }

        [HttpPut("UpdateFieldOptions")]
        public ActionResult UpdateFieldOptions(FieldOptions fieldOption)
        {
            if (fieldOption != null)
            {
                try
                {
                    maintenanceService.UpdateFieldOption(fieldOption);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the field option details")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper field option details")));
            }
        }

        [HttpDelete("DeleteFieldOptions")]
        public ActionResult DeleteFieldOptions(int id)
        {
            if (id > 0)
            {
                try
                {
                    maintenanceService.DeleteFieldOption(id);
                }
                catch(DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547) 
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e) 
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the field option details")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper field option details")));
            }
        }
        #endregion

    }
}
