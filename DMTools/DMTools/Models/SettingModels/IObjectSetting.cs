using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public interface IObjectSetting
    {
        string ID { get; }
        string Name { get; set; }
    }
}
