using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.DataEntities
{
    public class BaseEntity
    {
        [NotMapped]
        public int Id { get; set; }
    }
}
