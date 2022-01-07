using DMTools.Keys;
using DMTools.Models.CampaignModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DMTools.View.ContentViewer
{
    class ContentViewerSceneModel : ContentViewerViewModel
    {
        #region Variables and Properties

        SceneRepository Repository => SceneRepository.GetInstance();
        SessionRepository SessionRepository => SessionRepository.GetInstance();

        SceneModel m_model;

        public string WDW_Title { get => $"DM Tools - Adventure : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerSceneModel(SceneModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            AddText(result, $"Storyteller goal: {m_model.StorytellerGoal}");
            AddText(result, $"Players goal: {m_model.PlayerGoal}");
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            return result;
        }

        public override void Update()
        {
            var model = Repository.GetObjectById(m_model.ID);
            m_model = model;
            if (model == null) OnPropertyChanged(PropertyEventKeys.Close);
            else OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
