using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Maintenance
{
    public interface IMaintenanceService
    {

        InventoryResponseModel GetInventories();
        void InsertInventory(Inventories inventorie);
        void UpdateInventory(Inventories inventorie);
        void DeleteInventory(int id);

        EmployeeResponseModel GetEmployees();
        void InsertEmployee(Employees employee);
        void UpdateEmployee(Employees employees);
        void DeleteEmployee(int id);

        SalaryResponseModel GetSalaries();
        void InsertSalary(Salary salary);
        void UpdateSalary(Salary salary);
        void DeleteSalary(int id);

        SignatureResponseModel GetSignatures();
        void InsertSignature(SignaturePrototypes signature);
        void UpdateSignature(SignaturePrototypes signature);
        void DeleteSignature(int id);

        AbbrevationsResponseModel GetAbbrevations();
        void InsertAbbrevation(Abbrevations abbrevation);
        void UpdateAbbrevation(Abbrevations abbrevation);
        void DeleteAbbrevation(int id);

        FieldOptionsResponseModel GetFieldOptions();
        void InsertFieldOption(FieldOptions fieldOption);
        void UpdateFieldOption(FieldOptions fieldOption);
        void DeleteFieldOption(int id);

    }
}
