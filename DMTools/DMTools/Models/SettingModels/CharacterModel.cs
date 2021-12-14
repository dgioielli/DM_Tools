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
        public string CharacterName { get; set; }
        public string CharacterConcept { get; set; }
        public List<string> Notes { get; protected set; }


        #endregion

        #region Constructors

        public CharacterModel()
        {
            ID = "";
            CharacterName = "";
            CharacterConcept = "";
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Character > {CharacterName}";

        #endregion
    }
}
