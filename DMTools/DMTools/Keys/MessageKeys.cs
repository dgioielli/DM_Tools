using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Keys
{
    public static class MessageKeys
    {
        public static string GetDeleteMsg(string type, string name) => $"Do you want to permanetly delete the {type} \"{name}\"?";

    }
}
