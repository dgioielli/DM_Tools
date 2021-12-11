using DMTools.CoreLib.PoolItems;
using DMTools.Managers;
using DMTools.Models;
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

namespace DMTools.View.Campaign
{
    /// <summary>
    /// Lógica interna para CampaignView.xaml
    /// </summary>
    public partial class CampaignView : Window
    {
        #region Variables and Properties

        private const int KEY_NumColSections = 4;

        ObserverManager Observer => ObserverManager.GetInstance();
        Pool<SectionCellControl, SectionModel> m_poolSections = new Pool<SectionCellControl, SectionModel>();

        CampaignViewModel m_vm = new CampaignViewModel();

        #endregion

        #region Constructors

        public CampaignView()
        {
            InitializeComponent();
            Observer.RegisterGeneralObserver(m_vm);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            StartGridSection();
            ShowSections();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.LST_Sections)) ShowSections();
        }

        private void SetActions()
        {
            this.Closing += (sender, e) => Observer.QueueUnregisterGeneralObserver(m_vm);
        }

        #endregion

        #region Functions

        private void ShowSections()
        {
            SetGridSectionConfigurations();
            int n = m_vm.LST_Sections.Count;
            var controls = m_poolSections.GetObjects(m_vm.LST_Sections);
            for (int i = 0; i < n; i++)
                SetItemGrid(i / KEY_NumColSections, i % KEY_NumColSections, controls[i]);
            SetItemGrid(n / KEY_NumColSections, n % KEY_NumColSections, new Button() { Content = "New Section", Margin = new Thickness(5), Command = m_vm.BTN_NewSection });
        }

        private void SetItemGrid(int row, int col, Control control)
        {
            grd_sections.Children.Add(control);
            Grid.SetRow(control, row);
            Grid.SetColumn(control, col);
        }

        private void SetGridSectionConfigurations()
        {
            int qt = (int)((m_vm.LST_Sections.Count + 1) / KEY_NumColSections);
            if (qt < ((m_vm.LST_Sections.Count + 1) / ((double)KEY_NumColSections))) qt += 1;
            grd_sections.RowDefinitions.Clear();
            grd_sections.Children.Clear();
            for (int i = 0; i < qt; i++)
                grd_sections.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }

        private void StartGridSection()
        {
            grd_sections.ColumnDefinitions.Clear();
            for (int i = 0; i < KEY_NumColSections; i++)
                grd_sections.ColumnDefinitions.Add(new ColumnDefinition());
        }

        #endregion
    }
}
