using DMTools.Models.CampaignModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView.AdventureEditor
{
    class AdventureEditorViewModel : BaseObjectEditorViewModel<AdventureModel>
    {
        #region Variables and Properties

        AdventureRepository Repository => AdventureRepository.GetInstance();

        protected override string TypeEditor => "Adventure";

        public string TXT_Abstract { get => m_model.Abstract; set { m_model.Abstract = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.AdventureType; set { m_model.AdventureType = value; OnPropertyChanged(); } }
        
        #endregion

        #region Constructors

        public AdventureEditorViewModel(AdventureModel model) : base(model)
        { }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(TXT_Abstract));
            OnPropertyChanged(nameof(LST_Notes));
        }

        protected override void UpdateObject() => Repository.AddEditObject(m_model);

        #endregion
    }
}
