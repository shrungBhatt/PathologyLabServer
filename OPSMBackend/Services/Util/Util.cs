using OPSMBackend.DataEntities;
using OPSMBackend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Util
{
    public class Util : IUtil
    {
        public void CopyProperties(BaseEntity source, BaseEntity target)
        {
            PropertyInfo[] destinationProperties = target.GetType().GetProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);
                var ignoreCopy = false;
                foreach(var attribute in sourcePi.CustomAttributes)
                {
                    if(attribute.AttributeType.Name.Equals(nameof(IgnoreCopyAttribute)))
                    {
                        ignoreCopy = true;
                        break;
                    }
                }
                if (!ignoreCopy)
                {
                    destinationPi.SetValue(target, sourcePi.GetValue(source, null), null);
                }
            }
        }
    }
}
