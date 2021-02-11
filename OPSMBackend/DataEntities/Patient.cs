using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class Patient : BaseEntity
    {
        public int Id { get; set; }
        public string PatientCode { get; set; }
        public int? InitialId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AgeInYears { get; set; }
        public int AgeInMonths { get; set; }
        public int AgeInDays { get; set; }
        public int? Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Occupation { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ReferredBy { get; set; }
        public string Address { get; set; }
        public int? CivilStatus { get; set; }
        public bool IsFinished { get; set; }

        [NotMapped]
        public string PatientFullName { get; set; }
        [NotMapped]
        public string ReferredByFullName { get; set; }

        [NotMapped]
        public HdlRegistration ReferredByNavigation { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public List<TestResultsView> TestResults { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public List<TestTitles> TestTitles { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public PatientBill Bill { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public virtual FieldOptions CivilStatusNavigation { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public virtual Genders GenderNavigation { get; set; }
        [IgnoreCopy]
        [NotMapped]
        public FieldOptions OccupationNavigation { get; set; }

        [IgnoreCopy]
        [NotMapped]
        public virtual Initials Initial { get; set; }

        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<TestResults> TestResultsCollection { get; set; }

        [IgnoreCopy]
        [JsonIgnore]
        public virtual ICollection<PatientBill> PatientBill { get; set; }
        [JsonIgnore]
        public virtual ICollection<SpecializedLabSamples> SpecializedLabSamples { get; set; }
    }
}
