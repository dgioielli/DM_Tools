using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class CharacterEventModel
    {
        #region Variables and Properties

        public string CharacterId { get; set; }
        public string Info { get; set; }

        #endregion

        #region Constructors

        public CharacterEventModel() : this("", "")
        { }

        public CharacterEventModel(string characterId, string info)
        {
            CharacterId = characterId;
            Info = info;
        }

        public CharacterEventModel(CharacterEventModel x) : this(x.CharacterId, x.Info)
        { }

        #endregion

        #region Functions

        #endregion
    }
}
