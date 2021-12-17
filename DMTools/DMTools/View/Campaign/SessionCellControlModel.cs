using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Repositories;
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
    class SessionCellControlModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        SessionRepository Repository => SessionRepository.GetInstance();
        SessionModel m_model;

        public string TXT_SectionName { get => m_model.SessionName; set { m_model.SessionName = value; OnPropertyChanged(); } }

        public ICommand BTN_EditSection { get; protected set; }
        public ICommand BTN_DelSection { get; protected set; }
        public ICommand BTN_DuplicateSection { get; protected set; }

        #endregion

        #region Constructors

        public SessionCellControlModel(SessionModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(SessionModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_SectionName));
        }

        protected override void assinarComandos()
        {
            BTN_EditSection = new DGCommand(obj => UpdateSection());
            BTN_DelSection = new DGCommand(obj => DeleteSection());
            BTN_DuplicateSection = new DGCommand(obj => DuplicateSection());
            base.assinarComandos();
        }

        internal void ShowContent()
        {
            var dlg = new ContentViewerView(new ContentViewerSectionModel(m_model));
            dlg.Show();
        }

        private void DeleteSection()
        {
            if (MessageBox.Show($"Do you want to permanetly delete the section \"{TXT_SectionName}\"?", "DM Tools", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            Repository.DeleteSession(m_model);
        }

        private void UpdateSection()
        {
            var dlg = new SectionEditorView(Repository.GetCopy(m_model));
            dlg.Show();
        }

        private void DuplicateSection()
        {
            var dlg = new SectionEditorView(Repository.GetDuplicate(m_model));
            dlg.Show();
        }

        #endregion
    }
}
