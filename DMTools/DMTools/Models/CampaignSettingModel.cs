using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models
{
    public class CampaignSettingModel
    {
        #region Variables and Properties

        public string SettingName { get; set; }
        public List<CharacterModel> Characters { get; set; }
        public List<OrganizationModel> Organizations { get; set; }
        public List<LocationModel> Locations { get; set; }

        #endregion

        #region Constructors

        public CampaignSettingModel()
        {
            Characters = new List<CharacterModel>();
            Organizations = new List<OrganizationModel>();
            Locations = new List<LocationModel>();
        }

        #endregion

        #region Functions

        #endregion
    }
}
