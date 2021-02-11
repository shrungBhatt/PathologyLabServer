using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Dto
{
    public class TestComboListTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TestComboListTypeDto GetTestGroupDto(TestGroups testGroup)
        {
            var dto = new TestComboListTypeDto();
            dto.Id = testGroup.Id;
            dto.Name = testGroup.Name;

            return dto;
        }

        public TestComboListTypeDto GetTestTitleDto(TestTitles testTitle)
        {
            var dto = new TestComboListTypeDto();
            dto.Id = testTitle.Id;
            dto.Name = testTitle.Name;

            return dto;
        }
    }
}
