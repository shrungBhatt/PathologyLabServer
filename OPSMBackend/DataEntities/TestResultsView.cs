using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPSMBackend.DataEntities
{
    public partial class TestResultsView : BaseEntity
    {
        public int? PatientId { get; set; }
        public int GroupId { get; set; }
        public int TitleId { get; set; }
        public int OtherTestId { get; set; }
        public string GroupName { get; set; }
        public string TitleName { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public int TestResultId { get; set; }
        public string Unit { get; set; }
        public string NormalRange { get; set; }
        public string LargeTestResult { get; set; }

        [NotMapped]
        public Formulas Formula { get; set; }
        [NotMapped]
        public TestGroups TestGroup { get; set; }
        [NotMapped]
        public TestTitles TestTitle { get; set; }
        [NotMapped]
        public OtherTests OtherTest { get; set; }
        [NotMapped]
        public int OtherTestOrderId { get; set; }
        [NotMapped]
        public int TestGroupOrderId { get; set; }
        [NotMapped]
        public int TestTitleOrderId { get; set; } 
    }
}
