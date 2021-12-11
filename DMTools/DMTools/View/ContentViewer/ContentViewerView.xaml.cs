using DMTools.Keys;
using DMTools.Managers;
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

namespace DMTools.View.ContentViewer
{
    /// <summary>
    /// Lógica interna para ContentViewerView.xaml
    /// </summary>
    public partial class ContentViewerView : Window
    {
        #region Variables and Properties

        protected ObserverManager Observer => ObserverManager.GetInstance();

        ContentViewerViewModel m_vm;

        #endregion

        #region Constructors

        public ContentViewerView(ContentViewerViewModel vm)
        {
            InitializeComponent();
            m_vm = vm;
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            Observer.RegisterGeneralObserver(m_vm);
            SetActions();
            ShowDocument();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.GetDocument)) ShowDocument();
            else if (e.PropertyName == PropertyEventKeys.Close) Close();
        }

        private void SetActions()
        {
            this.Closing += (sender, e) => Observer.QueueUnregisterGeneralObserver(m_vm);
        }

        #endregion

        #region Functions

        private void ShowDocument()
        {
            txt_content.Document = m_vm.GetDocument();
        }

        #endregion
    }
}
