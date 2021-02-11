using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class TestGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int OrderNo { get; set; }
        public bool? ShowTitleInReports { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TestGroupDto GetTestGroupDto(TestGroups testGroups)
        {
            var dto = new TestGroupDto();
            dto.Id = testGroups.Id;
            dto.Name = testGroups.Name;
            dto.Notes = testGroups.Notes;
            dto.OrderNo = testGroups.OrderNo;
            dto.ShowTitleInReports = testGroups?.ShowTitleInReports;
            dto.ModifiedBy = testGroups?.ModifiedBy;
            dto.ModifiedDate = testGroups.ModifiedDate;

            return dto;
        }

        public TestGroups GetTestGroup(TestGroupDto testGroupDto)
        {
            var testGroup = new TestGroups();
            testGroup.Id = testGroupDto.Id;
            testGroup.Name = testGroupDto.Name;
            testGroup.Notes = testGroupDto.Notes;
            testGroup.OrderNo = testGroupDto.OrderNo;
            testGroup.ShowTitleInReports = testGroupDto?.ShowTitleInReports;
            testGroup.ModifiedBy = testGroupDto?.ModifiedBy;
            testGroup.ModifiedDate = DateTime.UtcNow;

            return testGroup;
        }

        public TestGroups GetTestGroup(TestGroups testGroup, TestGroupDto testGroupDto)
        {
            testGroup.Id = testGroupDto.Id;
            testGroup.Name = testGroupDto.Name;
            testGroup.Notes = testGroupDto.Notes;
            testGroup.OrderNo = testGroupDto.OrderNo;
            testGroup.ShowTitleInReports = testGroupDto?.ShowTitleInReports;
            testGroup.ModifiedBy = testGroupDto?.ModifiedBy;
            testGroup.ModifiedDate = DateTime.UtcNow;

            return testGroup;
        }
    }
}
