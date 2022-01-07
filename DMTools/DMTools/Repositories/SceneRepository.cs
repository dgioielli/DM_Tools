using DMTools.Models.CampaignModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    public class SceneRepository : ObjectBaseRepository<SceneModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static SceneRepository m_instance = new SceneRepository();

        public override List<SceneModel> Objects => Repository.Model.Scenes;

        #endregion

        #region Constructors

        public static SceneRepository GetInstance()
        { if (m_instance == null) m_instance = new SceneRepository(); return m_instance; }

        private SceneRepository()
        { m_update = Repository.Model.UpdateScenes; }

        #endregion

        #region Functions

        protected override void CopyInfo(SceneModel model, SceneModel result)
        {
            result.Name = model.Name;
            result.PlayerGoal = model.PlayerGoal;
            result.StorytellerGoal = model.StorytellerGoal;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        #endregion
    }
}
