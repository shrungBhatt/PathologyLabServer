﻿using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Models.Response
{
    public class BillResponseModel
    {
        public List<Patient> Patients { get; set; }
    }
}
