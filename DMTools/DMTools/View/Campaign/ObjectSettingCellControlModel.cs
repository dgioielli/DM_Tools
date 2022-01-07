using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DMTools.View.Campaign
{
    class ObjectSettingCellControlModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CharacterRepository Repository => CharacterRepository.GetInstance();
        ObjectSettingModel m_model;

        public string TXT_CharacterName { get => m_model.Object.ShowName; set { m_model.Object.Name = value; OnPropertyChanged(); } }

        public ICommand BTN_EditCharacter { get; protected set; }
        public ICommand BTN_DelCharacter { get; protected set; }
        public ICommand BTN_DuplicateCharacter { get; protected set; }

        #endregion

        #region Constructors

        public ObjectSettingCellControlModel(ObjectSettingModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(ObjectSettingModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_CharacterName));
        }

        protected override void assinarComandos()
        {
            BTN_EditCharacter = new DGCommand(obj => m_model.Update());
            BTN_DelCharacter = new DGCommand(obj => m_model.Delete());
            BTN_DuplicateCharacter = new DGCommand(obj => m_model.Duplicate());
            base.assinarComandos();
        }

        internal void ShowContent() => m_model.ShowContent();

        #endregion
    }
}
