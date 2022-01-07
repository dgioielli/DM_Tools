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

namespace DMTools.View.EditorView.EventEditor
{
    class EventCharacterCellControlModel : DGViewModel
    {
        #region Variables and Properties

        CharacterRepository CharRepository => CharacterRepository.GetInstance();

        CharacterEventModel m_model;

        public string TXT_Info { get => m_model.Info; set { m_model.Info = value; OnPropertyChanged(); } }
        public CharacterModel Character { get => CharRepository.GetObjectById(m_model.CharacterId); set => m_model.CharacterId = value.ID; }

        #endregion

        #region Constructors

        public EventCharacterCellControlModel(CharacterEventModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(CharacterEventModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_Info));
            OnPropertyChanged(nameof(Character));
        }

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        #endregion
    }
}
