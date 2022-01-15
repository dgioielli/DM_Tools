using DMTools.Models.CampaignModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView.SceneEditor
{
    class SceneEditorViewModel : BaseObjectEditorViewModel<SceneModel>
    {
        #region Variables and Properties

        SceneRepository Repository => SceneRepository.GetInstance();

        protected override string TypeEditor => "Scene";

        public string TXT_StorytellerGoal { get => m_model.StorytellerGoal; set { m_model.StorytellerGoal = value; OnPropertyChanged(); } }
        public string TXT_PlayersGoal { get => m_model.PlayerGoal; set { m_model.PlayerGoal = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.SceneType; set { m_model.SceneType = value; OnPropertyChanged(); } }

        #endregion

        #region Constructors

        public SceneEditorViewModel(SceneModel model) : base(model)
        { }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_StorytellerGoal));
            OnPropertyChanged(nameof(TXT_PlayersGoal));
            OnPropertyChanged(nameof(TXT_Type));
            base.Update();
        }

        protected override void UpdateObject() => Repository.AddEditObject(m_model);

        #endregion
    }
}
