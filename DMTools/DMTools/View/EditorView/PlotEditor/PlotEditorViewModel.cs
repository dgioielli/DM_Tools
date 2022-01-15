using DMTools.Models.CampaignModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView.PlotEditor
{
    class PlotEditorViewModel : BaseObjectEditorViewModel<PlotModel>
    {
        #region Variables and Properties

        PlotRepository Repository => PlotRepository.GetInstance();

        protected override string TypeEditor => "Plot";

        public string TXT_Abstract { get => m_model.Abstract; set { m_model.Abstract = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.PlotType; set { m_model.PlotType = value; OnPropertyChanged(); } }
        
        #endregion

        #region Constructors

        public PlotEditorViewModel(PlotModel model) : base(model)
        { }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Abstract));
            base.Update();
        }

        protected override void UpdateObject() => Repository.AddEditObject(m_model);

        #endregion
    }
}
