using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using DMTools.View.ContentViewer;
using DMTools.View.SectionEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DMTools.View.Campaign
{
    class CharacterCellControlModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CharacterRepository Repository => CharacterRepository.GetInstance();
        CharacterModel m_model;

        public string TXT_CharacterName { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); } }

        public ICommand BTN_EditCharacter { get; protected set; }
        public ICommand BTN_DelCharacter { get; protected set; }
        public ICommand BTN_DuplicateCharacter { get; protected set; }

        #endregion

        #region Constructors

        public CharacterCellControlModel(CharacterModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(CharacterModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_CharacterName));
        }

        protected override void assinarComandos()
        {
            BTN_EditCharacter = new DGCommand(obj => UpdateCharacter());
            BTN_DelCharacter = new DGCommand(obj => DeleteCharacter());
            BTN_DuplicateCharacter = new DGCommand(obj => DuplicateCharacter());
            base.assinarComandos();
        }

        internal void ShowContent()
        {
            var dlg = new ContentViewerView(new ContentViewerCharacterModel(m_model));
            dlg.Show();
        }

        private void DeleteCharacter()
        {
            if (MessageBox.Show($"Do you want to permanetly delete the section \"{TXT_CharacterName}\"?", "DM Tools", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            Repository.DeleteCharacter(m_model);
        }

        private void UpdateCharacter()
        {
            var dlg = new CharacterEditorView(Repository.GetCopy(m_model));
            dlg.Show();
        }

        private void DuplicateCharacter()
        {
            var dlg = new CharacterEditorView(Repository.GetDuplicate(m_model));
            dlg.Show();
        }

        #endregion
    }
}
