using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SessionModels
{
    public class SessionCharacterModel
    {
        #region Variables and Properties

        public string CharacterId { get; set; }
        public string Info { get; set; }

        #endregion

        #region Constructors

        public SessionCharacterModel() : this("", "")
        { }

        public SessionCharacterModel(string characterId, string info)
        {
            CharacterId = characterId;
            Info = info;
        }

        public SessionCharacterModel(SessionCharacterModel x) : this(x.CharacterId, x.Info)
        { }

        #endregion

        #region Functions

        #endregion
    }
}
