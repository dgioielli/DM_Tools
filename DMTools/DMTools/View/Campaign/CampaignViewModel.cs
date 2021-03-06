using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ListingView;
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
        SessionRepository SectionRepository => SessionRepository.GetInstance();
        CampaignModel m_model;

        public string TXT_CampaignName { get => m_model.CampaignName; set { m_model.CampaignName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string WDW_Title { get => $"DM Tools - Campaign : {m_model.CampaignName}"; }

        public List<SessionModel> LST_Sections { get => m_model.Sessions.OrderBy(x => x.SessionName).ToList(); }

        public ICommand BTN_SAVE { get; protected set; }
        public ICommand BTN_NewSection { get; protected set; }
        public ICommand BTN_Plots { get; protected set; }
        public ICommand BTN_Adventures { get; protected set; }
        public ICommand BTN_Scenes { get; protected set; }
        public ICommand BTN_Characters { get; protected set; }
        public ICommand BTN_Locations { get; protected set; }
        public ICommand BTN_Organizations { get; protected set; }
        public ICommand BTN_Events { get; protected set; }

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
            BTN_Plots = new DGCommand(obj => BaseListingView.Show(new PlotListingViewModel()));
            BTN_Adventures = new DGCommand(obj => BaseListingView.Show(new AdventureListingViewModel()));
            BTN_Scenes = new DGCommand(obj => BaseListingView.Show(new SceneListingViewModel()));
            BTN_Characters = new DGCommand(obj => BaseListingView.Show(new CharacterListingViewModel()));
            BTN_Locations = new DGCommand(obj => BaseListingView.Show(new LocationListingViewModel()));
            BTN_Organizations = new DGCommand(obj => BaseListingView.Show(new OrganizationListingViewModel()));
            BTN_Events = new DGCommand(obj => BaseListingView.Show(new EventListingViewModel()));
            base.assinarComandos();
        }

        private void NewSection()
        {
            var dlg = new SectionEditorView(SectionRepository.GetNewSession());
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
