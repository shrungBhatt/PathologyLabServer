﻿using Newtonsoft.Json;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;

namespace OPSMBackend.DataEntities
{
    public partial class Abbrevations : BaseEntity
    {
        public int Id { get; set; }
        public int OtherTestId { get; set; }
        public long SerialNo { get; set; }
        public string Abbreavation { get; set; }
        public string Interpretation { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [IgnoreCopy]
        public virtual OtherTests OtherTest { get; set; }
    }
}
