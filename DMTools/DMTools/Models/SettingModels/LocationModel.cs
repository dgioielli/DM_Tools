using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class LocationModel : IObjectSetting
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Concept { get; set; }
        public string OrganizationType { get; set; }
        public List<string> Notes { get; protected set; }


        #endregion

        #region Constructors

        public LocationModel()
        {
            ID = "";
            Name = "";
            Concept = "";
            OrganizationType = "";
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Character > {Name} :: {OrganizationType}//{Concept}";

        #endregion
    }
}
