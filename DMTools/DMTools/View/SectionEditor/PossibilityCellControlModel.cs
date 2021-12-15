using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SectionModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.SectionEditor
{
    class PossibilityCellControlModel : DGViewModel
    {
        #region Variables and Properties

        PossibilityModel m_model;

        public string TXT_PossibilityText { get => m_model.Text; set { m_model.Text = value; OnPropertyChanged(); } }
        public bool CHK_WasIgnored { get => m_model.WasIgnored; set { m_model.WasIgnored = value; OnPropertyChanged(); } }

        #endregion

        #region Constructors

        public PossibilityCellControlModel(PossibilityModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(PossibilityModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_PossibilityText));
            OnPropertyChanged(nameof(CHK_WasIgnored));
        }

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        #endregion
    }
}
