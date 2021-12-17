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

        #endregion

        #region Constructors

        public CampaignModel()
        {
            CampaignName = "";
            Setting = new CampaignSettingModel();
            Sessions = new List<SessionModel>();
        }

        #endregion

        #region Functions

        internal void Update()
        {
            Sessions = Sessions.OrderBy(x => x.ID).ToList();
        }

        #endregion
    }
}
