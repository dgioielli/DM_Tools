using DMTools.CoreLib.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMTools.View.LoadCampaign
{
    /// <summary>
    /// Interação lógica para CampaignRowUserControl.xam
    /// </summary>
    public partial class CampaignRowUserControl : UserControl
    {
        #region Variables and Properties

        OpenCampaignViewModel m_vm;

        public string TXT_CampaignName { get; set; }

        public ICommand BTN_OpenCampaign { get; protected set; }
        public ICommand BTN_DelCampaign { get; protected set; }

        #endregion

        #region Constructors

        public CampaignRowUserControl(string campaignName, OpenCampaignViewModel vm)
        {
            InitializeComponent();
            m_vm = vm;
            TXT_CampaignName = campaignName;
            SetCommands();
            DataContext = this;
        }

        #endregion

        #region Functions

        private void SetCommands()
        {
            BTN_OpenCampaign = new DGCommand(obj => m_vm.OpenCampaign(TXT_CampaignName));
            BTN_DelCampaign = new DGCommand(obj => m_vm.DelCampaign(TXT_CampaignName));
        }

        #endregion
    }
}
