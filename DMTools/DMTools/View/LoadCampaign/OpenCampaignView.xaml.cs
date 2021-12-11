using DMTools.Keys;
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
using System.Windows.Shapes;

namespace DMTools.View.LoadCampaign
{
    /// <summary>
    /// Lógica interna para OpenCampaignView.xaml
    /// </summary>
    public partial class OpenCampaignView : Window
    {
        #region Variables and Properties

        OpenCampaignViewModel m_vm = new OpenCampaignViewModel();

        #endregion

        #region Constructors

        public OpenCampaignView()
        {
            InitializeComponent();
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            DataContext = m_vm;
            ShowCampaigns();
        }

        #endregion

        #region Functions

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.LST_Campaigns)) ShowCampaigns();
            else if (e.PropertyName == nameof(m_vm.WDW_Visible)) Visibility = m_vm.WDW_Visible;
            else if (e.PropertyName == nameof(PropertyEventKeys.Close)) Close();
        }

        private void ShowCampaigns()
        {
            pnl_campaigns.Children.Clear();
            foreach (var item in m_vm.LST_Campaigns)
            {
                var uc = new CampaignRowUserControl(item, m_vm);
                pnl_campaigns.Children.Add(uc);
            }
        }

        #endregion
    }
}
