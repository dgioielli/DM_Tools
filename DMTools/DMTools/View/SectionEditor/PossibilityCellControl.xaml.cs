using DMTools.Models.SectionModels;
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

namespace DMTools.View.SectionEditor
{
    /// <summary>
    /// Interação lógica para PossibilityCellControl.xam
    /// </summary>
    public partial class PossibilityCellControl : UserControl
    {
        #region Variables and Properties

        PossibilityCellControlModel m_vm;
        Action m_onChanged;

        #endregion

        #region Constructors

        public PossibilityCellControl(PossibilityModel model, Action onChanged)
        {
            InitializeComponent();
            m_onChanged = onChanged;
            m_vm = new PossibilityCellControlModel(model);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void SetActions()
        {
            txt_possibility.OnTextChanged = m_onChanged;
        }

        #endregion

        #region Functions

        public void Update(PossibilityModel obj) => m_vm.Update(obj);

        #endregion
    }
}
