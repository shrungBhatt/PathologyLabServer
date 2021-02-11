using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Finance
{
    public interface IFinanceService
    {
        OtherIncomeResponseModel GetOtherIncome();
        void InsertOtherIncome(OtherIncome otherIncome);
        void UpdateOtherIncome(OtherIncome otherIncome);
        void DeleteOtherIncome(int id);

        ExpensesResponseModel GetExpenses();
        void InsertExpense(Expenses expense);
        void UpdateExpense(Expenses expense);
        void DeleteExpense(int id);

        SalaryPaymentResponseModel GetSalaryPayments();
        void InsertSalaryPayment(SalaryPayment salaryPayment);
        void UpdateSalarypayment(SalaryPayment salaryPayment);

        void InsertPatientBill(PatientBill patientBill);
        void UpdatePatientBill(PatientBill patientBill);
        void DeletePatientBill(int id);

        PatientBillPaymentResponseModel GetBillPayments();
        void InsertBillPayment(PatientBillPayment billPayment);
        void UpdateBillPayment(PatientBillPayment billPayment);
        void DeleteBillPayment(int id);

        HdlBillResponseModel GetHdlBills();
        void InsertHdlBill(HdlBill bill);
        void UpdateHdlBill(HdlBill bill);
        void DeleteHdlBill(int id);

        HdlBillPaymentResponseModel GetHdlBillPayments();
        void InsertHdlBillPayment(HdlBillPayment hdlBillPayment);
        void UpdateHdlBillPayment(HdlBillPayment hdlBillPayment);
        void DeleteHdlBillPayment(int id);

        PatientRevenueReportResponseModel GetPatientRevenueReport(PatientRevenueReportResponseModel model);
        PatientBillPaymentReportResponeModel GetPatientBillPaymentReport(PatientBillPaymentReportResponeModel model);
    }
}
