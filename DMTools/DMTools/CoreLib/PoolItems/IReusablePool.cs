using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.PoolItems
{
    public interface IReusablePool <U>
    {
        void Update(U obj);
    }
}
