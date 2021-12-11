using DMTools.CoreLib.ViewModel;
using DMTools.Repositories;
using DMTools.View.Campaign;
using DMTools.View.LoadCampaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DMTools.View
{
    internal class MainWindowViewModel : DGViewModel
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private Visibility m_visible = Visibility.Visible;

        public Visibility WDW_Visible { get => m_visible; set { m_visible = value; OnPropertyChanged(); } }

        public ICommand BTN_ContinueCurrentCampaign { get; protected set; }
        public ICommand BTN_NewCampaign { get; protected set; }
        public ICommand BTN_OpenCampaign { get; protected set; }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        { }

        #endregion

        #region Functions

        protected override void assinarComandos()
        {
            BTN_ContinueCurrentCampaign = new DGCommand(obj => ContinueCurrentCampaign());
            BTN_NewCampaign = new DGCommand(obj => NewCampaign());
            BTN_OpenCampaign = new DGCommand(obj => OpenCampaign());
            base.assinarComandos();
        }

        private void OpenCampaign()
        {
            WDW_Visible = Visibility.Hidden;
            var dlg = new OpenCampaignView();
            dlg.ShowDialog();
            WDW_Visible = Visibility.Visible;
        }

        private void NewCampaign()
        {
            WDW_Visible = Visibility.Hidden;
            Repository.SetNewCampaign();
            var dlg = new CampaignView();
            dlg.ShowDialog();
            WDW_Visible = Visibility.Visible;
        }

        private void ContinueCurrentCampaign()
        {
            WDW_Visible = Visibility.Hidden;
            var dlg = new CampaignView();
            dlg.ShowDialog();
            WDW_Visible = Visibility.Visible;
        }

        #endregion
    }
}
