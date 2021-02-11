using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class Employees : BaseEntity
    {
        public Employees()
        {
            Salaries = new HashSet<Salary>();
            SalaryPayment = new HashSet<SalaryPayment>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int EmpCategoryId { get; set; }
        public int FieldOptionsId { get; set; }
        public int EmployeeRoleId { get; set; }
        public int Supervisor { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public Salary Salary { get; set; }

        [IgnoreCopy]
        public virtual EmployeeCategories EmpCategory { get; set; }
        [IgnoreCopy]
        public virtual EmployeeRoles EmployeeRole { get; set; }
        [IgnoreCopy]
        public virtual FieldOptions FieldOptions { get; set; }
        [IgnoreCopy]
        public virtual Genders Gender { get; set; }

        [JsonIgnore]
        public virtual ICollection<Salary> Salaries { get; set; }
        [JsonIgnore]
        public virtual ICollection<SalaryPayment> SalaryPayment { get; set; }
    }
}
