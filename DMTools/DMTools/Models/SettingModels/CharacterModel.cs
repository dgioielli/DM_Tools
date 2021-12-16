using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class CharacterModel
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Concept { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public List<string> Notes { get; protected set; }


        #endregion

        #region Constructors

        public CharacterModel()
        {
            ID = "";
            Name = "";
            Concept = "";
            Race = "";
            Class = "";
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Character > {Name} :: {Race}//{Class}";

        #endregion
    }
}
