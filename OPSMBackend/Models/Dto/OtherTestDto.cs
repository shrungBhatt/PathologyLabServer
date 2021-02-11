using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class OtherTestDto
    {
        public int Id { get; set; }
        public int TestGroupId { get; set; }
        public int TestTitleId { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public string Unit { get; set; }
        public bool? DisplayInTestResult { get; set; }
        public bool? DisplayInBoldFontInReport { get; set; }
        public bool? DescriptiveResult { get; set; }
        public string ValMale { get; set; }
        public string ValFemale { get; set; }
        public string ValNoenatal { get; set; }
        public string ValChild { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DefaultValue { get; set; }
        public string Options { get; set; }


        public OtherTestDto GetOtherTestDto(OtherTests otherTest)
        {
            var dto = new OtherTestDto();
            dto.Id = otherTest.Id;
            dto.TestGroupId = otherTest.TestGroupId;
            dto.TestTitleId = otherTest.TestTitleId;
            dto.Name = otherTest.Name;
            dto.OrderBy = otherTest.OrderBy;
            dto.Unit = otherTest.Unit;
            dto.DisplayInTestResult = otherTest?.DisplayInTestResult;
            dto.DisplayInBoldFontInReport = otherTest?.DisplayInBoldFontInReport;
            dto.DescriptiveResult = otherTest?.DescriptiveResult;
            dto.ValMale = otherTest?.ValMale;
            dto.ValFemale = otherTest?.ValFemale;
            dto.ValNoenatal = otherTest?.ValNoenatal;
            dto.ValChild = otherTest?.ValChild;
            dto.DefaultValue = otherTest?.DefaultValue;
            dto.Options = otherTest?.Options;
            dto.ModifiedBy = otherTest.ModifiedBy;
            dto.ModifiedDate = otherTest.ModifiedDate;

            return dto;
        }

        public OtherTests GetOtherTest(OtherTestDto otherTestDto)
        {
            var otherTest = new OtherTests();
            otherTest.Id = otherTestDto.Id;
            otherTest.TestGroupId = otherTestDto.TestGroupId;
            otherTest.TestTitleId = otherTestDto.TestTitleId;
            otherTest.Name = otherTestDto.Name;
            otherTest.OrderBy = otherTestDto.OrderBy;
            otherTest.Unit = otherTestDto.Unit;
            otherTest.DisplayInTestResult = otherTestDto?.DisplayInTestResult;
            otherTest.DisplayInBoldFontInReport = otherTestDto?.DisplayInBoldFontInReport;
            otherTest.DescriptiveResult = otherTestDto?.DescriptiveResult;
            otherTest.ValMale = otherTestDto?.ValMale;
            otherTest.ValFemale = otherTestDto?.ValFemale;
            otherTest.ValNoenatal = otherTestDto?.ValNoenatal;
            otherTest.ValChild = otherTestDto?.ValChild;
            otherTest.DefaultValue = otherTestDto?.DefaultValue;
            otherTest.Options = otherTestDto?.Options;
            otherTest.ModifiedBy = otherTestDto.ModifiedBy;
            otherTest.ModifiedDate = DateTime.UtcNow;

            return otherTest;

        }

        public OtherTests GetOtherTest(OtherTests otherTest, OtherTestDto otherTestDto)
        {
            otherTest.Id = otherTestDto.Id;
            otherTest.TestGroupId = otherTestDto.TestGroupId;
            otherTest.TestTitleId = otherTestDto.TestTitleId;
            otherTest.Name = otherTestDto.Name;
            otherTest.OrderBy = otherTestDto.OrderBy;
            otherTest.Unit = otherTestDto.Unit;
            otherTest.DisplayInTestResult = otherTestDto?.DisplayInTestResult;
            otherTest.DisplayInBoldFontInReport = otherTestDto?.DisplayInBoldFontInReport;
            otherTest.DescriptiveResult = otherTestDto?.DescriptiveResult;
            otherTest.ValMale = otherTestDto?.ValMale;
            otherTest.ValFemale = otherTestDto?.ValFemale;
            otherTest.ValNoenatal = otherTestDto?.ValNoenatal;
            otherTest.ValChild = otherTestDto?.ValChild;
            otherTest.DefaultValue = otherTestDto?.DefaultValue;
            otherTest.Options = otherTestDto?.Options;
            otherTest.ModifiedBy = otherTestDto.ModifiedBy;
            otherTest.ModifiedDate = DateTime.UtcNow;

            return otherTest;
        }
    }
}
