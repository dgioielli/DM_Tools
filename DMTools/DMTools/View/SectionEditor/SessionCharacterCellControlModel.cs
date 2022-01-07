using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Models.SessionModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.SectionEditor
{
    class SessionCharacterCellControlModel : DGViewModel
    {
        #region Variables and Properties

        CharacterRepository CharRepository => CharacterRepository.GetInstance();

        SessionCharacterModel m_model;

        public string TXT_Info { get => m_model.Info; set { m_model.Info = value; OnPropertyChanged(); } }
        public CharacterModel Character { get => CharRepository.GetObjectById(m_model.CharacterId); set => m_model.CharacterId = value.ID; }
        public int CBO_Index_Role { get => (int)m_model.Role; set { m_model.Role = (ECharacterRoleKeys)value; OnPropertyChanged(); } }

        #endregion

        #region Constructors

        public SessionCharacterCellControlModel(SessionCharacterModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(SessionCharacterModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_Info));
            OnPropertyChanged(nameof(Character));
            OnPropertyChanged(nameof(CBO_Index_Role));
        }

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        #endregion
    }
}
