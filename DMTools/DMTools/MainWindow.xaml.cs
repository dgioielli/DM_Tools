using DMTools.View;
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

namespace DMTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables and Properties

        MainWindowViewModel m_vm = new MainWindowViewModel();

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.WDW_Visible)) Visibility = m_vm.WDW_Visible;
        }

        #endregion
    }
}
