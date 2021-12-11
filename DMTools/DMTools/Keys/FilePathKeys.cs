using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Keys
{
    class FilePathKeys
    {
        public static string EXTdata => ".data";

        public static string AppDataDir => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string AppPluginDir => $"{AppDataDir}\\DM_Tools\\";
        public static string AppLogsDir => $"{AppPluginDir}logs\\";
        public static string AppCampaignsDir => $"{AppPluginDir}Campaigns\\";

        public static string ConfigAppFile => $"{AppPluginDir}DMToolsConfig.json";
        public static string SystemLayersFile => $"{AppPluginDir}CurrentCampaign.data";
    }
}
