using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class LocationRepository
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static LocationRepository m_instance = new LocationRepository();

        public List<LocationModel> Organizations { get => Repository.Model.Setting.Locations; }

        #endregion

        #region Constructors

        public static LocationRepository GetInstance()
        { if (m_instance == null) m_instance = new LocationRepository(); return m_instance; }

        private LocationRepository()
        { }

        #endregion

        #region Functions

        public LocationModel GetNewCharacter()
        {
            var result = new LocationModel() { ID = GetNewID() };
            return result;
        }

        private string GetNewID()
        {
            var now = DateTime.Now;
            return $"Character:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        internal void AddEditCharacter(LocationModel model)
        {
            if (!Organizations.Exists(x => x.ID == model.ID)) AddCharacter(model);
            else EditCharacter(model, Organizations.Find(x => x.ID == model.ID));
        }

        internal LocationModel GetCharacterById(string id) => Organizations.Find(x => x.ID == id);

        private void EditCharacter(LocationModel model, LocationModel oldModel)
        {
            Organizations.Remove(oldModel);
            AddCharacter(model);
        }

        private void AddCharacter(LocationModel model)
        {
            Organizations.Add(model);
            Repository.Model.Update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteCharacter(LocationModel model)
        {
            Organizations.Remove(model);
            Observer.UpdateGeneralObserver();
        }

        internal LocationModel GetCopy(LocationModel model)
        {
            var result = new LocationModel() { ID = model.ID };
            CopyInfo(model, result);
            return result;
        }

        internal LocationModel GetDuplicate(LocationModel model)
        {
            var result = new LocationModel() { ID = GetNewID() };
            CopyInfo(model, result);
            return result;
        }

        private static void CopyInfo(LocationModel model, LocationModel result)
        {
            result.Name = model.Name;
            result.Concept = model.Concept;
            result.OrganizationType = model.OrganizationType;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        internal List<string> GetAllTypes() => GetAllData(x => x.OrganizationType);

        protected List<string> GetAllData(Func<LocationModel, string> getData)
        {
            var result = new List<string>();
            Organizations.ForEach(x => result.Add(getData(x)));
            return result.Distinct().OrderBy(x => x).ToList();
        }

        #endregion
    }
}
