using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using OPSMBackend.Repositories;
using OPSMBackend.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Maintenance
{
    public class MaintenanceService : IMaintenanceService
    {
        readonly IRepository<Inventories> _inventoriesRepository;
        readonly IRepository<Employees> _employeesRepository;
        readonly IRepository<Salary> _salaryRepository;
        readonly IRepository<SignaturePrototypes> _signaturesRepository;
        readonly IRepository<Abbrevations> _abbreavationsRepository;
        readonly IRepository<FieldOptions> _fieldOptionsRepository;
        readonly IRepository<OtherTests> _testRepository;
        readonly IRepository<Fields> _fieldsRepository;
        readonly IRepository<Genders> _gendersRepository;
        readonly IRepository<EmployeeCategories> _empCategoriesRepository;
        readonly IRepository<EmployeeRoles> _empRolesRepository;
        readonly IUtil _util;

        public MaintenanceService(IRepository<Inventories> inventoriesRepository,
                                  IRepository<Employees> employeesRepository,
                                  IRepository<Salary> salaryRepository,
                                  IRepository<SignaturePrototypes> signaturesRepository,
                                  IRepository<Abbrevations> abbreavationsRepository,
                                  IRepository<FieldOptions> fieldOptionsRepository,
                                  IRepository<OtherTests> testRepository,
                                  IRepository<Fields> fieldsRepository,
                                  IRepository<Genders> gendersRepository,
                                  IRepository<EmployeeCategories> empCategoriesRepository,
                                  IRepository<EmployeeRoles> empRolesRepository,
                                  IUtil util)
        {
            _inventoriesRepository = inventoriesRepository;
            _employeesRepository = employeesRepository;
            _salaryRepository = salaryRepository;
            _signaturesRepository = signaturesRepository;
            _abbreavationsRepository = abbreavationsRepository;
            _fieldOptionsRepository = fieldOptionsRepository;
            _testRepository = testRepository;
            _fieldsRepository = fieldsRepository;
            _gendersRepository = gendersRepository;
            _empCategoriesRepository = empCategoriesRepository;
            _empRolesRepository = empRolesRepository;
            _util = util;
        }

        public void DeleteAbbrevation(int id)
        {
            _abbreavationsRepository.Delete(_abbreavationsRepository.Get(id));
        }

        public void DeleteEmployee(int id)
        {
            _employeesRepository.Delete(_employeesRepository.Get(id));
        }

        public void DeleteFieldOption(int id)
        {
            _fieldOptionsRepository.Delete(_fieldOptionsRepository.Get(id));
        }

        public void DeleteInventory(int id)
        {
            _inventoriesRepository.Delete(_inventoriesRepository.Get(id));
        }

        public void DeleteSalary(int id)
        {
            _salaryRepository.Delete(_salaryRepository.Get(id));
        }

        public void DeleteSignature(int id)
        {
            _signaturesRepository.Delete(_signaturesRepository.Get(id));
        }

        public AbbrevationsResponseModel GetAbbrevations()
        {
            var tests = _testRepository.GetAll().ToList();
            var abbrevations = _abbreavationsRepository.GetAll().ToList();

            return new AbbrevationsResponseModel { Abbrevations = abbrevations, OtherTests = tests };
        }

        public EmployeeResponseModel GetEmployees()
        {
            var civilStatus = _fieldOptionsRepository.GetAll().ToList().FindAll(x => x.FieldId == 3);
            var empRoles = _empRolesRepository.GetAll().ToList();
            var genders = _gendersRepository.GetAll().ToList();
            var empCategories = _empCategoriesRepository.GetAll().ToList();
            var employees = _employeesRepository.GetAll().ToList();

            return new EmployeeResponseModel
            {
                Employees = employees,
                EmployeeCategories = empCategories,
                EmployeeRoles = empRoles,
                Genders = genders,
                CivilStatuses = civilStatus
            };
        }

        public FieldOptionsResponseModel GetFieldOptions()
        {
            var fields = _fieldsRepository.GetAll().ToList();
            var fieldOptions = _fieldOptionsRepository.GetAll().ToList();

            return new FieldOptionsResponseModel { FieldOptions = fieldOptions, Fields = fields };
        }

        public InventoryResponseModel GetInventories()
        {
            return new InventoryResponseModel { Inventories = _inventoriesRepository.GetAll().ToList() };
        }

        public SalaryResponseModel GetSalaries()
        {
            var employees = _employeesRepository.GetAll().ToList();
            var salaries = _salaryRepository.GetAll().ToList();

            return new SalaryResponseModel { Employees = employees, Salaries = salaries };
        }

        public SignatureResponseModel GetSignatures()
        {
            return new SignatureResponseModel { Signatures = _signaturesRepository.GetAll().ToList() };
        }

        public void InsertAbbrevation(Abbrevations abbrevation)
        {
            _abbreavationsRepository.Insert(abbrevation);
        }

        public void InsertEmployee(Employees employee)
        {
            _employeesRepository.Insert(employee);
        }

        public void InsertFieldOption(FieldOptions fieldOption)
        {
            _fieldOptionsRepository.Insert(fieldOption);
        }

        public void InsertInventory(Inventories inventory)
        {
            _inventoriesRepository.Insert(inventory);
        }

        public void InsertSalary(Salary salary)
        {
            _salaryRepository.Insert(salary);
        }

        public void InsertSignature(SignaturePrototypes signature)
        {
            _signaturesRepository.Insert(signature);
        }

        public void UpdateAbbrevation(Abbrevations abbrevation)
        {
            var abbrevationFromDb = _abbreavationsRepository.Get(abbrevation.Id);
            if (abbrevationFromDb != null)
            {
                _util.CopyProperties(abbrevation, abbrevationFromDb);
                _abbreavationsRepository.Update(abbrevationFromDb);
            }
            else
            {
                throw new Exception("This abbrevation is not available");
            }
        }

        public void UpdateEmployee(Employees employees)
        {
            var employeeFromDb = _employeesRepository.Get(employees.Id);
            if (employeeFromDb != null)
            {
                _util.CopyProperties(employees, employeeFromDb);
                _employeesRepository.Update(employeeFromDb);
            }
            else
            {
                throw new Exception("This employee does not exist");
            }
        }

        public void UpdateFieldOption(FieldOptions fieldOption)
        {
            var fieldOptionFromDb = _fieldOptionsRepository.Get(fieldOption.Id);
            if (fieldOptionFromDb != null)
            {
                _util.CopyProperties(fieldOption, fieldOptionFromDb);
                _fieldOptionsRepository.Update(fieldOptionFromDb);
            }
            else
            {
                throw new Exception("This field option does not exist");
            }
        }

        public void UpdateInventory(Inventories inventory)
        {
            var inventoryFromDb = _inventoriesRepository.Get(inventory.Id);
            if (inventoryFromDb != null)
            {
                _util.CopyProperties(inventory, inventoryFromDb);
                _inventoriesRepository.Update(inventoryFromDb);
            }
            else
            {
                throw new Exception("This inventory item does not exist");
            }
        }

        public void UpdateSalary(Salary salary)
        {
            var salaryFromDb = _salaryRepository.Get(salary.Id);
            if (salaryFromDb != null)
            {
                _util.CopyProperties(salary, salaryFromDb);
                _salaryRepository.Update(salaryFromDb);
            }
            else
            {
                throw new Exception("This salary does not exist");
            }
        }

        public void UpdateSignature(SignaturePrototypes signature)
        {
            var signatureFromDb = _signaturesRepository.Get(signature.Id);
            if (signatureFromDb != null)
            {
                _util.CopyProperties(signature, signatureFromDb);
                _signaturesRepository.Update(signatureFromDb);
            }
            else
            {
                throw new Exception("This signature does not exist");
            }
        }
    }
}
