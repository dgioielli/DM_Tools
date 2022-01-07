using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models
{
    public interface IObjectBase
    {
        string ID { get; set; }
        string Name { get; set; }

        string ShowName { get; }
    }
}
