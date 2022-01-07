using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class LocationRepository : ObjectBaseRepository<LocationModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static LocationRepository m_instance = new LocationRepository();

        public override List<LocationModel> Objects => Repository.Model.Setting.Locations;

        #endregion

        #region Constructors

        public static LocationRepository GetInstance()
        { if (m_instance == null) m_instance = new LocationRepository(); return m_instance; }

        private LocationRepository()
        { m_update = Repository.Model.Setting.UpdateLocations; }

        #endregion

        #region Functions

        protected override void CopyInfo(LocationModel model, LocationModel result)
        {
            result.Name = model.Name;
            result.Concept = model.Concept;
            result.LocationType = model.LocationType;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        public List<string> GetAllTypes() => GetAllData(x => x.LocationType);

        public List<string> GetAllShowNames() => GetAllData(x => x.ShowName);

        #endregion
    }
}
