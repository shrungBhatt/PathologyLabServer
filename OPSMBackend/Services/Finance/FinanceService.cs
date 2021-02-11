using OPSMBackend.DataEntities;
using OPSMBackend.Models.Dto;
using OPSMBackend.Models.Response;
using OPSMBackend.Repositories;
using OPSMBackend.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Finance
{
    public class FinanceService : IFinanceService
    {
        readonly IRepository<OtherIncome> otherIncomeRepository;
        readonly IRepository<AccountHead> accountHeadReposiory;
        readonly IRepository<Expenses> expensesRepository;
        readonly IRepository<ExpensesAccountHead> expAccountHeadRepository;
        readonly IRepository<SalaryPayment> salaryPaymentRepositroy;
        readonly IRepository<Employees> employeesRepository;
        readonly IRepository<Salary> salariesRepository;
        readonly IRepository<PatientBillPayment> billPaymentRepository;
        readonly IRepository<DataEntities.Patient> patientRepository;
        readonly IRepository<FieldOptions> fieldOptionsRepository;
        readonly IRepository<PatientBill> patientBillRepository;
        readonly IRepository<HdlRegistration> hdlRegistrationRepository;
        readonly IRepository<HdlBill> hdlBillRepository;
        readonly IRepository<HdlBillPayment> hdlBillPaymentRepository;
        readonly IRepository<Users> usersRepository;

        readonly IUtil _util;

        public FinanceService(IRepository<OtherIncome> otherIncomeRepository,
                              IRepository<AccountHead> accountHeadReposiory,
                              IRepository<ExpensesAccountHead> expAccountHeadRepository,
                              IRepository<Expenses> expensesRepository,
                              IRepository<SalaryPayment> salaryPaymentRepositroy,
                              IRepository<Employees> employeesRepository,
                              IRepository<Salary> salariesRepository,
                              IRepository<PatientBill> patientBillRepository,
                              IRepository<PatientBillPayment> billPaymentRepository,
                              IRepository<DataEntities.Patient> patientRepository,
                              IRepository<FieldOptions> fieldOptionsRepository,
                              IRepository<HdlRegistration> hdlRegistrationRepository,
                              IRepository<HdlBill> hdlBillRepository,
                              IRepository<HdlBillPayment> hdlBillPaymentRepository,
                              IRepository<Users> usersRepository,
                              IUtil util)
        {
            this.otherIncomeRepository = otherIncomeRepository;
            this.accountHeadReposiory = accountHeadReposiory;
            this.expAccountHeadRepository = expAccountHeadRepository;
            this.expensesRepository = expensesRepository;
            this.salariesRepository = salariesRepository;
            this.employeesRepository = employeesRepository;
            this.salaryPaymentRepositroy = salaryPaymentRepositroy;
            this.patientBillRepository = patientBillRepository;
            this.billPaymentRepository = billPaymentRepository;
            this.patientRepository = patientRepository;
            this.fieldOptionsRepository = fieldOptionsRepository;
            this.hdlRegistrationRepository = hdlRegistrationRepository;
            this.hdlBillRepository = hdlBillRepository;
            this.hdlBillPaymentRepository = hdlBillPaymentRepository;
            this.usersRepository = usersRepository;
            _util = util;
        }

        public void DeleteBillPayment(int id)
        {
            billPaymentRepository.Delete(billPaymentRepository.Get(id));
        }

        public void DeleteExpense(int id)
        {
            expensesRepository.Delete(expensesRepository.Get(id));
        }

        public void DeleteHdlBill(int id)
        {
            hdlBillRepository.Delete(hdlBillRepository.Get(id));
        }

        public void DeleteHdlBillPayment(int id)
        {
            hdlBillPaymentRepository.Delete(hdlBillPaymentRepository.Get(id));
        }

        public void DeleteOtherIncome(int id)
        {
            otherIncomeRepository.Delete(otherIncomeRepository.Get(id));
        }

        public void DeletePatientBill(int id)
        {
            patientBillRepository.Delete(patientBillRepository.Get(id));
        }

        public PatientBillPaymentResponseModel GetBillPayments()
        {
            var patients = patientRepository.GetAll().ToList();
            foreach (var patient in patients)
            {
                patient.Bill = patientBillRepository.GetAll().ToList()?.Find(x => x.PatientId == patient.Id);
            }
            var patientBillPayments = billPaymentRepository.GetAll().ToList();
            var paymentTypes = fieldOptionsRepository.GetAll().ToList().FindAll(x => x.FieldId == 4);

            return new PatientBillPaymentResponseModel { BillPayments = patientBillPayments, PaymentTypes = paymentTypes, Patients = patients };
        }

        public ExpensesResponseModel GetExpenses()
        {
            var expAccountHeads = expAccountHeadRepository.GetAll().ToList();
            var expenses = expensesRepository.GetAll().ToList();

            return new ExpensesResponseModel { Expenses = expenses, ExpensesAccountHeads = expAccountHeads };
        }

        public HdlBillPaymentResponseModel GetHdlBillPayments()
        {
            var hdls = hdlRegistrationRepository.GetAll().Where(x => x.RegistrationCategoryId == 1 && x.RegistrationTypeId != 3)?.ToList();
            foreach (var hdl in hdls)
            {
                hdl.Bill = hdlBillRepository.GetAll().ToList().Find(x => x.HdlId == hdl.Id);
            }
            var hdlBillPayments = hdlBillPaymentRepository.GetAll().ToList();
            var paymentTypes = fieldOptionsRepository.GetAll().ToList().FindAll(x => x.FieldId == 4);

            return new HdlBillPaymentResponseModel { HdlBillPayments = hdlBillPayments, HdlRegistrations = hdls, PaymentTypes = paymentTypes };
        }

        public HdlBillResponseModel GetHdlBills()
        {
            var model = new HdlBillResponseModel();
            var hdls = hdlRegistrationRepository.GetAll().Where(x => x.RegistrationCategoryId == 1 && x.RegistrationTypeId != 3)?.ToList();
            if (hdls != null && hdls.Count > 0)
            {
                foreach (var hdl in hdls)
                {
                    hdl.Bill = hdlBillRepository.GetAll()?.ToList()?.Find(x => x.HdlId == hdl.Id);
                    model.HdlRegistrations.Add(hdl);
                }
            }

            return model;
        }

        public OtherIncomeResponseModel GetOtherIncome()
        {
            var accountHeads = accountHeadReposiory.GetAll().ToList();
            var otherIncomes = otherIncomeRepository.GetAll().ToList();

            return new OtherIncomeResponseModel { AccountHeads = accountHeads, OtherIncomes = otherIncomes };
        }

        public PatientRevenueReportResponseModel GetPatientRevenueReport(PatientRevenueReportResponseModel model)
        {
            if (model.FromDate == null || model.ToDate == null)
            {
                throw new Exception("Please enter proper date range");
            }

            model.Hdls = hdlRegistrationRepository.GetAll().ToList()?.FindAll(x => x.RegistrationCategoryId == 2 && x.RegistrationTypeId != 3);
            model.Occupations = fieldOptionsRepository.GetAll().ToList()?.FindAll(x => x.FieldId == 2);

            var bills = patientBillRepository.GetAll().ToList()?.FindAll(x => x.BillDate >= model.FromDate && x.BillDate <= model.ToDate);
            if (bills != null)
            {
                model.RevenueItems = new List<PatientRevenueModelDto>();
                foreach (var bill in bills)
                {
                    var item = new PatientRevenueModelDto();
                    item.BillId = bill.Id;
                    item.BillNo = (int)bill.BillNo;
                    var discount = (float)bill.Discount;
                    var gst = (float)bill.Gst;
                    item.TotalAmount = (float)bill.TotalCharges - ConvertPercentToValue((float)bill.TotalCharges, discount) + ConvertPercentToValue((float)bill.TotalCharges, gst);
                    item.AmountPaid = (float)bill.AmountPaid;
                    item.Balance = (float) (item.TotalAmount - (float)bill.AmountPaid);
                    item.Balance = (float)Math.Round(item.Balance, 2);
                    item.IsPaid = bill.AmountPaid >= item.TotalAmount;

                    var patient = patientRepository.Get(bill.PatientId);
                    if (patient != null)
                    {
                        item.PatientCode = patient.PatientCode;
                        item.PatientName = patient.FirstName + " " + patient.LastName;
                        item.PatientId = patient.Id;
                        item.HdlId = (int)patient.ReferredBy;
                        item.Occupation = patient.Occupation;
                    }
                    model.RevenueItems.Add(item);
                }
            }

            return model;
        }

        float ConvertPercentToValue(float amount, float percent)
        {
            return ((float)amount * percent) / 100;
        }
        public SalaryPaymentResponseModel GetSalaryPayments()
        {
            var employees = employeesRepository.GetAll().ToList();
            var salaries = salariesRepository.GetAll().ToList();
            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    employee.Salary = salaries?.Find(x => x.EmployeeId == employee.Id);
                }
            }
            var salaryPayments = salaryPaymentRepositroy.GetAll().ToList();

            return new SalaryPaymentResponseModel { Employees = employees, SalaryPayments = salaryPayments };
        }

        public void InsertBillPayment(PatientBillPayment billPayment)
        {
            billPaymentRepository.Insert(billPayment);

            var billFromDb = patientBillRepository.Get(billPayment.BillId);
            if (billFromDb != null)
            {
                billFromDb.AmountPaid = (double?)billPayment.PaymentAmount;
                billFromDb.Balance = (double?)billPayment.Balance;
                patientBillRepository.Update(billFromDb);
            }

        }

        public void InsertExpense(Expenses expense)
        {
            expensesRepository.Insert(expense);
        }

        public void InsertHdlBill(HdlBill bill)
        {
            hdlBillRepository.Insert(bill);
        }

        public void InsertHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            hdlBillPaymentRepository.Insert(hdlBillPayment);

            var billFromDb = hdlBillRepository.Get(hdlBillPayment.BillId);
            if (billFromDb != null)
            {
                billFromDb.AmountPaid = hdlBillPayment.PaymentAmount;
                billFromDb.Balance = hdlBillPayment.Balance;
                hdlBillRepository.Update(billFromDb);
            }

        }

        public void InsertOtherIncome(OtherIncome otherIncome)
        {
            otherIncomeRepository.Insert(otherIncome);
        }

        public void InsertPatientBill(PatientBill patientBill)
        {
            patientBillRepository.Insert(patientBill);
        }

        public void InsertSalaryPayment(SalaryPayment salaryPayment)
        {
            salaryPaymentRepositroy.Insert(salaryPayment);
        }

        public void UpdateBillPayment(PatientBillPayment billPayment)
        {
            var patientBillPaymentFromDb = billPaymentRepository.Get(billPayment.Id);
            if (patientBillPaymentFromDb != null)
            {
                _util.CopyProperties(billPayment, patientBillPaymentFromDb);
                billPaymentRepository.Update(patientBillPaymentFromDb);

                var billFromDb = patientBillRepository.Get(billPayment.BillId);
                if (billFromDb != null)
                {
                    billFromDb.AmountPaid = (double?)billPayment.PaymentAmount;
                    billFromDb.Balance = (double?)billPayment.Balance;
                    patientBillRepository.Update(billFromDb);
                }

            }
            else
            {
                throw new Exception("This patient bill payment record does not exist");
            }
        }

        public void UpdateExpense(Expenses expense)
        {
            var expenseFromDb = expensesRepository.Get(expense.Id);
            if (expenseFromDb != null)
            {
                _util.CopyProperties(expense, expenseFromDb);
                expensesRepository.Update(expenseFromDb);
            }
            else
            {
                throw new Exception("This expense record does not exist");
            }
        }

        public void UpdateHdlBill(HdlBill bill)
        {
            var hdlBillFromDb = hdlBillRepository.Get(bill.Id);
            if (hdlBillFromDb != null)
            {
                _util.CopyProperties(bill, hdlBillFromDb);
                hdlBillRepository.Update(hdlBillFromDb);
            }
            else
            {
                throw new Exception("This Doctor/Hospital bill does not exist");
            }
        }

        public void UpdateHdlBillPayment(HdlBillPayment hdlBillPayment)
        {
            var hdlBillPaymentFromDb = hdlBillPaymentRepository.Get(hdlBillPayment.Id);
            if (hdlBillPaymentFromDb != null)
            {
                _util.CopyProperties(hdlBillPayment, hdlBillPaymentFromDb);
                hdlBillPaymentRepository.Update(hdlBillPaymentFromDb);

                var billFromDb = hdlBillRepository.Get(hdlBillPayment.BillId);
                if (billFromDb != null)
                {
                    billFromDb.AmountPaid = hdlBillPayment.PaymentAmount;
                    billFromDb.Balance = hdlBillPayment.Balance;
                    hdlBillRepository.Update(billFromDb);
                }

            }
            else
            {
                throw new Exception("This patient bill payment record does not exist");
            }
        }

        public void UpdateOtherIncome(OtherIncome otherIncome)
        {
            var otherIncomeFromDb = otherIncomeRepository.Get(otherIncome.Id);
            if (otherIncomeFromDb != null)
            {
                _util.CopyProperties(otherIncome, otherIncomeFromDb);
                otherIncomeRepository.Update(otherIncomeFromDb);
            }
            else
            {
                throw new Exception("This other income record does not exist");
            }
        }

        public void UpdatePatientBill(PatientBill patientBill)
        {
            var patientBillFromDb = patientBillRepository.Get(patientBill.Id);
            if (patientBillFromDb != null)
            {
                _util.CopyProperties(patientBill, patientBillFromDb);
                patientBillRepository.Update(patientBillFromDb);
            }
            else
            {
                throw new Exception("This patient's bill does not exist");
            }
        }

        public void UpdateSalarypayment(SalaryPayment salaryPayment)
        {
            var salaryPaymentFromDb = salaryPaymentRepositroy.Get(salaryPayment.Id);
            if (salaryPaymentFromDb != null)
            {
                _util.CopyProperties(salaryPayment, salaryPaymentFromDb);
                salaryPaymentRepositroy.Update(salaryPaymentFromDb);
            }
            else
            {
                throw new Exception("This salary payment record does not exist");
            }
        }

        public PatientBillPaymentReportResponeModel GetPatientBillPaymentReport(PatientBillPaymentReportResponeModel model)
        {
            if (model.FromDate == null || model.ToDate == null)
            {
                throw new Exception("Please enter proper date range");
            }

            model.Users = usersRepository.GetAll().ToList();
            model.PaymentTypes = fieldOptionsRepository.GetAll().ToList()?.FindAll(x => x.FieldId == 4);

            var billPayments = billPaymentRepository.GetAll().ToList()?.FindAll(x => x.PaymentDate >= model.FromDate && x.PaymentDate <= model.ToDate);
            if (billPayments != null)
            {
                model.BillPayments = new List<PatientBillPaymentReportDto>();
                foreach (var billPayment in billPayments)
                {
                    var item = new PatientBillPaymentReportDto();
                    item.BillPaymentId = billPayment.Id;
                    item.ReceiptNo = (int)billPayment.ReceiptNo;
                    item.CardMode = (bool)billPayment.CardMode;
                    item.ChequeMode = (bool)billPayment.ChequeMode;
                    item.CashMode = (bool)billPayment.CashMode;
                    item.PaymentDate = billPayment.PaymentDate.Value;
                    item.PaymentTypeId = billPayment.PaymentType;
                    item.BillPaymentType = fieldOptionsRepository.Get(billPayment.PaymentType);
                    item.Amount = (float)billPayment.PaymentAmount;
                    item.PaidBy = billPayment.BillPaidBy;
                    item.EntryDoneBy = billPayment.ModifiedBy;

                    var bill = patientBillRepository.Get(billPayment.BillId);
                    var patient = patientRepository.Get(bill.PatientId);
                    if (patient != null)
                    {
                        item.PatientCode = patient.PatientCode;
                        item.PatientName = patient.FirstName + " " + patient.LastName;
                        item.PatientId = patient.Id;
                    }
                    model.BillPayments.Add(item);
                }
            }

            return model;
        }
    }
}
