using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.CampaignModels
{
    public class AdventureModel : IObjectBase
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string AdventureType { get; set; }
        public List<string> Notes { get; protected set; }

        [JsonIgnore]
        public string ShowName => Name;


        #endregion

        #region Constructors

        public AdventureModel()
        {
            ID = "";
            Name = "";
            Abstract = "";
            AdventureType = "";
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Character > {Name} :: {AdventureType}//{Abstract}";

        #endregion
    }
}
