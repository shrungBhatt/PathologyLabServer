using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Util
{
    public interface IUtil
    {
        void CopyProperties(BaseEntity source, BaseEntity target);
    }
}
