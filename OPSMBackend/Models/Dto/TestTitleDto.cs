using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class TestTitleDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int? OrderBy { get; set; }
        public int Charges { get; set; }
        public string HeaderNote { get; set; }
        public string FooterNote { get; set; }
        public bool WordFormatResult { get; set; }
        public bool ShowNormalRangeHeader { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TestTitleDto GetTestTitleDto(TestTitles testTitle)
        {
            var dto = new TestTitleDto();
            dto.Id = testTitle.Id;
            dto.GroupId = testTitle.GroupId;
            dto.Name = testTitle.Name;
            dto.Charges = testTitle.Charges;
            dto.OrderBy = testTitle.OrderBy;
            dto.HeaderNote = testTitle.HeaderNote;
            dto.FooterNote = testTitle.FooterNote;
            dto.WordFormatResult = testTitle.WordFormatResult;
            dto.ShowNormalRangeHeader = testTitle.ShowNormalRangeHeader;
            dto.ModifiedBy = testTitle.ModifiedBy;
            dto.ModifiedDate = testTitle.ModifiedDate;

            return dto;
        }

        public TestTitles GetTestTile(TestTitleDto testTitleDto)
        {
            var testTitle = new TestTitles();
            testTitle.Id = testTitleDto.Id;
            testTitle.GroupId = testTitleDto.GroupId;
            testTitle.Name = testTitleDto.Name;
            testTitle.Charges = testTitleDto.Charges;
            testTitle.OrderBy = testTitleDto.OrderBy;
            testTitle.HeaderNote = testTitleDto.HeaderNote;
            testTitle.FooterNote = testTitleDto.FooterNote;
            testTitle.WordFormatResult = testTitleDto.WordFormatResult;
            testTitle.ShowNormalRangeHeader = testTitleDto.ShowNormalRangeHeader;
            testTitle.ModifiedBy = testTitleDto.ModifiedBy;
            testTitle.ModifiedDate = DateTime.UtcNow;

            return testTitle;
        }

        public TestTitles GetTestTitle(TestTitles testTitle, TestTitleDto testTitleDto)
        {
            testTitle.Id = testTitleDto.Id;
            testTitle.GroupId = testTitleDto.GroupId;
            testTitle.Name = testTitleDto.Name;
            testTitle.Charges = testTitleDto.Charges;
            testTitle.OrderBy = testTitleDto.OrderBy;
            testTitle.HeaderNote = testTitleDto.HeaderNote;
            testTitle.FooterNote = testTitleDto.FooterNote;
            testTitle.WordFormatResult = testTitleDto.WordFormatResult;
            testTitle.ShowNormalRangeHeader = testTitleDto.ShowNormalRangeHeader;
            testTitle.ModifiedBy = testTitleDto.ModifiedBy;
            testTitle.ModifiedDate = DateTime.UtcNow;

            return testTitle;
        }

    }
}
