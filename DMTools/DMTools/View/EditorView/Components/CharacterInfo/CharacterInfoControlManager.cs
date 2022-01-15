using DMTools.CoreLib.PoolItems;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DMTools.View.EditorView.Components
{
    class CharacterInfoControlManager
    {
        #region Variables and Properties

        PoolGeneric<CharacterInfoCellControl, ObjectInfoModel> m_poolCharacters;
        Action m_update;

        #endregion

        #region Constructors

        public CharacterInfoControlManager(Action update)
        {
            m_update = update;
            m_poolCharacters = new PoolGeneric<CharacterInfoCellControl, ObjectInfoModel>(NewCharacter, RefreshCharacter);
        }

        #endregion

        #region Functions

        public void ShowCharacters(StackPanel pnl, List<ObjectInfoModel> characters)
        {
            var list = m_poolCharacters.GetObjects(characters);
            pnl.Children.Clear();
            list.ForEach(x => pnl.Children.Add(x));
        }

        protected CharacterInfoCellControl NewCharacter(ObjectInfoModel arg) => new CharacterInfoCellControl(arg, m_update);

        protected CharacterInfoCellControl RefreshCharacter(CharacterInfoCellControl arg1, ObjectInfoModel arg2)
        {
            arg1.Update(arg2);
            return arg1;
        }

        #endregion
    }
}
