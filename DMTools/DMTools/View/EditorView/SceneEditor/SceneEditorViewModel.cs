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
    class SceneEditorViewModel : EditorViewModel
    {
        #region Variables and Properties

        SceneRepository Repository => SceneRepository.GetInstance();
        SceneModel m_model;

        protected override string TypeEditor => "Scene";

        public override string TXT_Name { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_StorytellerGoal { get => m_model.StorytellerGoal; set { m_model.StorytellerGoal = value; OnPropertyChanged(); } }
        public string TXT_PlayersGoal { get => m_model.PlayerGoal; set { m_model.PlayerGoal = value; OnPropertyChanged(); } }
        public List<string> LST_Notes { get => m_model.Notes; }

        #endregion

        #region Constructors

        public SceneEditorViewModel(SceneModel model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(TXT_StorytellerGoal));
            OnPropertyChanged(nameof(TXT_PlayersGoal));
            OnPropertyChanged(nameof(LST_Notes));
        }

        protected override void UpdateObject() => Repository.AddEditObject(m_model);

        public void SetNotes(List<string> notes)
        {
            m_model.Notes.Clear();
            foreach (var note in notes)
                if (note != "") m_model.Notes.Add(note);
            OnPropertyChanged(nameof(LST_Notes));
        }

        #endregion
    }
}
