using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Util
{
    [DisplayName("IgnoreCopy")]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreCopyAttribute : Attribute
    {
    }
}
