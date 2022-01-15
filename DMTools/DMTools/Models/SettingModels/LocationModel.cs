using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class LocationModel : IObjectBase
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Concept { get; set; }
        public string Description { get; set; }
        public string LocationType { get; set; }
        public List<ObjectInfoModel> NotableCharacters { get; protected set; }
        public List<string> Notes { get; protected set; }

        [JsonIgnore]
        public string ShowName => $"{LocationType} :: {Name}";


        #endregion

        #region Constructors

        public LocationModel()
        {
            ID = "";
            Name = "";
            Concept = "";
            LocationType = "";
            Notes = new List<string>();
            NotableCharacters = new List<ObjectInfoModel>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Location > {Name} :: {LocationType}//{Concept}";

        #endregion
    }
}
