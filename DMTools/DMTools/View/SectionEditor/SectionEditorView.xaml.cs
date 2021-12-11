using DMTools.Keys;
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

namespace DMTools.View.SectionEditor
{
    /// <summary>
    /// Lógica interna para SectionEditorView.xaml
    /// </summary>
    public partial class SectionEditorView : Window
    {
        #region Variables and Properties

        SectionEditorViewModel m_vm;

        #endregion

        #region Constructors

        public SectionEditorView(SectionModel model)
        {
            InitializeComponent();
            m_vm = new SectionEditorViewModel(model);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyEventKeys.Close) Close();
        }

        private void SetActions()
        { }

        #endregion

        #region Functions

        #endregion
    }
}
