using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Models;
using DMTools.Repositories;
using DMTools.Services;
using DMTools.View.Campaign;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DMTools.View.LoadCampaign
{
    public class OpenCampaignViewModel : DGViewModel
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        List<string> m_campaignList = new List<string>();

        private Visibility m_visible = Visibility.Visible;

        public Visibility WDW_Visible { get => m_visible; set { m_visible = value; OnPropertyChanged(); } }
        public List<string> LST_Campaigns { get => m_campaignList; }

        #endregion

        #region Constructors

        public OpenCampaignViewModel()
        {
            Reset();
        }

        #endregion

        #region Functions

        private void Reset()
        {
            m_campaignList = DirectoryService.GetFileNames(FilePathKeys.AppCampaignsDir, FilePathKeys.EXTdata);
            OnPropertyChanged(nameof(LST_Campaigns));
        }

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        internal void OpenCampaign(string campaignName)
        {
            WDW_Visible = Visibility.Hidden;
            Repository.LoadData($"{FilePathKeys.AppCampaignsDir}{campaignName}{FilePathKeys.EXTdata}");
            var dlg = new CampaignView();
            dlg.ShowDialog();
            WDW_Visible = Visibility.Visible;
            OnPropertyChanged(PropertyEventKeys.Close);
        }

        internal void DelCampaign(string campaignName)
        {
            if (MessageBox.Show($"Do you want to permanetly delete the campaign \"{campaignName}\"?", "DM Tools", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            File.Delete($"{FilePathKeys.AppCampaignsDir}{campaignName}{FilePathKeys.EXTdata}");
            Reset();
        }

        #endregion
    }
}
