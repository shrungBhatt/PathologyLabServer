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
using OPSMBackend.Services.Finance;

namespace OPSMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : BaseController
    {
        readonly IFinanceService financeService;

        public FinanceController(IFinanceService financeService)
        {
            this.financeService = financeService;
        }

        #region Other income
        [HttpGet("GetOtherIncomes")]
        public ActionResult GetOtherIncomes()
        {
            var responseModel = financeService.GetOtherIncome();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an other income record")));
            }
        }

        [HttpPost("NewOtherIncome")]
        public ActionResult NewOtherIncome(OtherIncome otherIncome)
        {
            if (otherIncome != null)
            {
                try
                {
                    financeService.InsertOtherIncome(otherIncome);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new other income record")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other income details")));
            }
        }

        [HttpPut("UpdateOtherIncome")]
        public ActionResult UpdateOtherIncome(OtherIncome otherIncome)
        {
            if (otherIncome != null)
            {
                try
                {
                    financeService.UpdateOtherIncome(otherIncome);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the other income record")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other income record")));
            }
        }

        [HttpDelete("DeleteOtherIncome")]
        public ActionResult DeleteOtherIncome(int id)
        {
            if (id > 0)
            {
                try
                {
                    financeService.DeleteOtherIncome(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the other income record")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper other income record")));
            }
        }
        #endregion

        #region Expenses
        [HttpGet("GetExpenses")]
        public ActionResult GetExpenses()
        {
            var responseModel = financeService.GetExpenses();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an expense record")));
            }
        }

        [HttpPost("NewExpense")]
        public ActionResult NewExpense(Expenses expense)
        {
            if (expense != null)
            {
                try
                {
                    financeService.InsertExpense(expense);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new expense record")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper expense details")));
            }
        }

        [HttpPut("UpdateExpense")]
        public ActionResult UpdateExpense(Expenses expense)
        {
            if (expense != null)
            {
                try
                {
                    financeService.UpdateExpense(expense);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the expense record")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper expense record")));
            }
        }

        [HttpDelete("DeleteExpense")]
        public ActionResult DeleteExpense(int id)
        {
            if (id > 0)
            {
                try
                {
                    financeService.DeleteExpense(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the expense record")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper expense record")));
            }
        }
        #endregion

        #region Salary payment
        [HttpGet("GetSalaryPayments")]
        public ActionResult GetSalaryPayments()
        {
            var responseModel = financeService.GetSalaryPayments();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an salary payment record")));
            }
        }

        [HttpPost("NewSalaryPayment")]
        public ActionResult NewSalaryPayment(SalaryPayment salaryPayment)
        {
            if (salaryPayment != null)
            {
                try
                {
                    financeService.InsertSalaryPayment(salaryPayment);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new salary payment record")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper salary payment details")));
            }
        }

        [HttpPut("UpdateSalaryPayment")]
        public ActionResult UpdateSalaryPayment(SalaryPayment salaryPayment)
        {
            if (salaryPayment != null)
            {
                try
                {
                    financeService.UpdateSalarypayment(salaryPayment);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the salary payment record")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper salary payment details")));
            }
        }

        #endregion

        #region Patient bill
        [HttpPost("NewPatientBill")]
        public ActionResult NewPatientBill(PatientBill patientBill)
        {
            if (patientBill != null)
            {
                try
                {
                    financeService.InsertPatientBill(patientBill);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new bill")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill details")));
            }
        }

        [HttpPut("UpdatePatientBill")]
        public ActionResult UpdatePatientBill(PatientBill patientBill)
        {
            if (patientBill != null)
            {
                try
                {
                    financeService.UpdatePatientBill(patientBill);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the bill")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill details")));
            }
        }

        [HttpDelete("DeletePatientBill")]
        public ActionResult DeletePatientBill(int id)
        {
            if (id > 0)
            {
                try
                {
                    financeService.DeletePatientBill(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the patient bill")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill details")));
            }
        }
        #endregion

        #region Patient bill payment
        [HttpGet("GetPatientBillPayments")]
        public ActionResult GetPatientBillPayments()
        {
            var responseModel = financeService.GetBillPayments();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an patient bill payment record")));
            }
        }

        [HttpPost("NewPatientBillPayment")]
        public ActionResult NewPatientBillPayment(PatientBillPayment patientBillPayment)
        {
            if (patientBillPayment != null)
            {
                try
                {
                    financeService.InsertBillPayment(patientBillPayment);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new patient bill payment record")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient bill payment details")));
            }
        }

        [HttpPut("UpdatePatientBillPayment")]
        public ActionResult UpdatePatientBillPayment(PatientBillPayment patientBillPayment)
        {
            if (patientBillPayment != null)
            {
                try
                {
                    financeService.UpdateBillPayment(patientBillPayment);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the patient bill payment record")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient bill payment details")));
            }
        }

        [HttpDelete("DeletePatientBillPayment")]
        public ActionResult DeletePatientBillPayment(int id)
        {
            if (id > 0)
            {
                try
                {
                    financeService.DeleteBillPayment(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the patient bill payment record")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper patient bill payment record")));
            }
        }
        #endregion

        #region Hdl bill
        [HttpGet("GetHdlBills")]
        public ActionResult GetHdlBills()
        {
            var responseModel = financeService.GetHdlBills();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an other income record")));
            }
        }


        [HttpPost("NewHdlBill")]
        public ActionResult NewHdlBill(HdlBill hdlBill)
        {
            if (hdlBill != null)
            {
                try
                {
                    financeService.InsertHdlBill(hdlBill);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new bill")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill details")));
            }
        }

        [HttpPut("UpdateHdlBill")]
        public ActionResult UpdateHdlBill(HdlBill hdlBill)
        {
            if (hdlBill != null)
            {
                try
                {
                    financeService.UpdateHdlBill(hdlBill);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the bill")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill details")));
            }
        }

        [HttpDelete("DeleteHdlBill")]
        public ActionResult DeleteHdlBill(int id)
        {
            if (id > 0)
            {
                try
                {
                    financeService.DeleteHdlBill(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the bill")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill details")));
            }
        }
        #endregion

        #region Hdl bill payment
        [HttpGet("GetHdlBillPayments")]
        public ActionResult GetHdlBillPayments()
        {
            var responseModel = financeService.GetHdlBillPayments();
            if (responseModel != null)
            {
                return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, responseModel));
            }
            else
            {
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No data found", "Please add an bill payment record")));
            }
        }

        [HttpPost("NewHdlBillPayment")]
        public ActionResult NewHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            if (hdlBillPayment != null)
            {
                try
                {
                    financeService.InsertHdlBillPayment(hdlBillPayment);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while creating new bill payment record")));
                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter patient bill payment details")));
            }
        }

        [HttpPut("UpdateHdlBillPayment")]
        public ActionResult UpdateHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            if (hdlBillPayment != null)
            {
                try
                {
                    financeService.UpdateHdlBillPayment(hdlBillPayment);
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while updating the bill payment record")));
                }
                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill payment details")));
            }
        }

        [HttpDelete("DeleteHdlBillPayment")]
        public ActionResult DeleteHdlBillPayment(int id)
        {
            if (id > 0)
            {
                try
                {
                    financeService.DeleteHdlBillPayment(id);
                }
                catch (DbUpdateException ex) when ((ex.GetBaseException() as SqlException).Number == 547)
                {
                    Program.Logger.Error(ex);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.foreignConstraintFailed, "Failed", "This field is being used by some other reference. Please delete the reference first before deleting this.")));
                }
                catch (Exception e)
                {
                    Program.Logger.Error(e);
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", "Error occurred while deleting the bill payment record")));

                }

                return Ok(GetResponse(ResponseType.ACK, ResponseStatusCode.SUCCESS));
            }
            else
            {
                return BadRequest(GetResponse(ResponseType.ERROR, ResponseStatusCode.ERROR, GetError(ErrorCodes.invalidData, "Invalid input", "Please enter proper bill payment record")));
            }
        }
        #endregion

        [HttpPost("GetPatientRevenueReport")]
        public ActionResult GetPatientRevenueReport(PatientRevenueReportResponseModel requestModel)
        {
            try
            {
                var resposeModel = financeService.GetPatientRevenueReport(requestModel);

                if (resposeModel != null)
                {
                    return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No bills found", "Something went wrong!")));
                }
            }
            catch (Exception e)
            {
                Program.Logger.Error(e);
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", e.Message)));
            }

        }

        [HttpPost("GetPatientBillPaymentReport")]
        public ActionResult GetPatientBillPaymentReport(PatientBillPaymentReportResponeModel requestModel)
        {
            try
            {
                var resposeModel = financeService.GetPatientBillPaymentReport(requestModel);

                if (resposeModel != null)
                {
                    return Ok(GetResponse(ResponseType.OBJECT, ResponseStatusCode.SUCCESS, resposeModel));
                }
                else
                {
                    return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "No bills found", "Something went wrong!")));
                }
            }
            catch (Exception e)
            {
                Program.Logger.Error(e);
                return Ok(GetResponse(ResponseType.FAIL, ResponseStatusCode.FAIL, GetError(ErrorCodes.dataNotFound, "Failed", e.Message)));
            }

        }
    }
}
