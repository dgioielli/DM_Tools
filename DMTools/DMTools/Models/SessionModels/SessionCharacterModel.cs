using DMTools.Keys;
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
        public ECharacterRoleKeys Role { get; set; }

        #endregion

        #region Constructors

        public SessionCharacterModel() : this("", "", ECharacterRoleKeys.Secondary)
        { }

        public SessionCharacterModel(string characterId, string info, ECharacterRoleKeys role)
        {
            CharacterId = characterId;
            Info = info;
            Role = role;
        }

        public SessionCharacterModel(SessionCharacterModel x) : this(x.CharacterId, x.Info, x.Role)
        { }

        #endregion

        #region Functions

        #endregion
    }
}
