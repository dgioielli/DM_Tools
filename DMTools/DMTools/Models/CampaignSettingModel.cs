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
        public List<EventModel> Events { get; set; }

        #endregion

        #region Constructors

        public CampaignSettingModel()
        {
            Characters = new List<CharacterModel>();
            Organizations = new List<OrganizationModel>();
            Locations = new List<LocationModel>();
            Events = new List<EventModel>();
        }

        #endregion

        #region Functions

        internal void UpdateEvents() => Events = Events.OrderBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.Day).ThenBy(x => x.Name).ToList();

        internal void UpdateCharacters() => Characters = Characters.OrderBy(x => x.Name).ToList();

        internal void UpdateLocations() => Locations = Locations.OrderBy(x => x.LocationType).ThenBy(x => x.Name).ToList();

        internal void UpdateOrganizations() => Organizations = Organizations.OrderBy(x => x.OrganizationType).ThenBy(x => x.Name).ToList();

        #endregion
    }
}
