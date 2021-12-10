using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Services
{
    public static class JsonService
    {
        public static string toJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static T toT<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
