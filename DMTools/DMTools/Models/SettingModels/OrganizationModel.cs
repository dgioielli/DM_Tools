using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class OrganizationModel : IObjectBase
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Concept { get; set; }
        public string OrganizationType { get; set; }
        public List<ObjectInfoModel> Members { get; protected set; }
        public List<string> Notes { get; protected set; }

        [JsonIgnore]
        public string ShowName => $"{OrganizationType} :: {Name}";


        #endregion

        #region Constructors

        public OrganizationModel()
        {
            ID = "";
            Name = "";
            Concept = "";
            OrganizationType = "";
            Notes = new List<string>();
            Members = new List<ObjectInfoModel>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Character > {Name} :: {OrganizationType}//{Concept}";

        #endregion
    }
}
