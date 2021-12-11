using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Repositories;
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
    class CampaignViewModel : DGViewModel, IGeneralObserver
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();
        SectionRepository SectionRepository => SectionRepository.GetInstance();
        CampaignModel m_model;

        public string TXT_CampaignName { get => m_model.CampaignName; set { m_model.CampaignName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string WDW_Title { get => $"DM Tools - Campaign : {m_model.CampaignName}"; }

        public List<SectionModel> LST_Sections { get => m_model.Sections; }

        public ICommand BTN_SAVE { get; protected set; }
        public ICommand BTN_NewSection { get; protected set; }

        #endregion

        #region Constructors

        public CampaignViewModel()
        {
            m_model = Repository.Model;
        }

        #endregion

        #region Functions

        protected override void assinarComandos()
        {
            BTN_SAVE = new DGCommand(obj => SaveCampaign());
            BTN_NewSection = new DGCommand(obj => NewSection());
            base.assinarComandos();
        }

        private void NewSection()
        {
            var dlg = new SectionEditorView(SectionRepository.GetNewSection());
            dlg.Show();
        }

        private void SaveCampaign()
        {
            Repository.SaveCurrent();
            Repository.SaveData($"{FilePathKeys.AppCampaignsDir}{m_model.CampaignName}{FilePathKeys.EXTdata}");
        }

        public void Update()
        {
            OnPropertyChanged(nameof(LST_Sections));
        }

        #endregion
    }
}
