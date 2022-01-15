using DMTools.Models.CampaignModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models
{
    public class CampaignModel
    {
        #region Variables and Properties

        public string CampaignName { get; set; }
        public CampaignSettingModel Setting { get; set; }
        public List<SessionModel> Sessions { get; protected set; }
        public List<AdventureModel> Adventures { get; set; }
        public List<PlotModel> Plots { get; set; }
        public List<SceneModel> Scenes { get; set; }

        #endregion

        #region Constructors

        public CampaignModel()
        {
            CampaignName = "";
            Setting = new CampaignSettingModel();
            Sessions = new List<SessionModel>();
            Adventures = new List<AdventureModel>();
            Plots = new List<PlotModel>();
            Scenes = new List<SceneModel>();
        }

        #endregion

        #region Functions

        internal void UpdateSessions() => Sessions = Sessions.OrderBy(x => x.SessionName).ToList();

        internal void UpdateAdventures() => Adventures = Adventures.OrderBy(x => x.AdventureType).ThenBy(x => x.Name).ToList();

        internal void UpdatePlots() => Plots = Plots.OrderBy(x => x.PlotType).ThenBy(x => x.Name).ToList();

        internal void UpdateScenes() => Scenes = Scenes.OrderBy(x => x.SceneType).ThenBy(x => x.Name).ToList();

        #endregion
    }
}
