using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class CharacterModel : IObjectBase
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Concept { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string Clan { get; set; }
        public List<string> Notes { get; protected set; }

        [JsonIgnore]
        public string ShowName => Name;


        #endregion

        #region Constructors

        public CharacterModel()
        {
            ID = "";
            Name = "";
            Concept = "";
            Race = "";
            Class = "";
            Clan = "";
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Character > {Name} :: {Race}//{Class}";

        #endregion
    }
}
