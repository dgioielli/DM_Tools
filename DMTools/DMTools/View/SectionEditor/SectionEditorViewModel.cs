using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.SectionEditor
{
    class SectionEditorViewModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        SectionRepository Repository => SectionRepository.GetInstance();
        SectionModel m_model;

        public string TXT_SectionName { get => m_model.SectionName; set { m_model.SectionName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_SectionIntro { get { if (m_model.SectionIntro == "") return "..."; return m_model.SectionIntro; } set { m_model.SectionIntro = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string WDW_Title { get => $"DM Tools - Section : {m_model.SectionName}"; }

        public ICommand BTN_Update { get; protected set; }
        public ICommand BTN_Conclude { get; protected set; }
        public ICommand BTN_Cancel { get; protected set; }

        #endregion

        #region Constructors

        public SectionEditorViewModel(SectionModel model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        protected override void assinarComandos()
        {
            BTN_Update = new DGCommand(obj => UpdateSection());
            BTN_Conclude = new DGCommand(obj => { UpdateSection(); OnPropertyChanged(PropertyEventKeys.Close); });
            BTN_Cancel = new DGCommand(obj => OnPropertyChanged(PropertyEventKeys.Close));
            base.assinarComandos();
        }

        private void UpdateSection()
        {
            Repository.AddEditSection(m_model);
        }

        #endregion
    }
}
