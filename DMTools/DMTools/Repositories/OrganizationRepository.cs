using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class OrganizationRepository
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static OrganizationRepository m_instance = new OrganizationRepository();

        public List<OrganizationModel> Organizations { get => Repository.Model.Setting.Organizations; }

        #endregion

        #region Constructors

        public static OrganizationRepository GetInstance()
        { if (m_instance == null) m_instance = new OrganizationRepository(); return m_instance; }

        private OrganizationRepository()
        { }

        #endregion

        #region Functions

        public OrganizationModel GetNewCharacter()
        {
            var result = new OrganizationModel() { ID = GetNewID() };
            return result;
        }

        private string GetNewID()
        {
            var now = DateTime.Now;
            return $"Character:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        internal void AddEditCharacter(OrganizationModel model)
        {
            if (!Organizations.Exists(x => x.ID == model.ID)) AddCharacter(model);
            else EditCharacter(model, Organizations.Find(x => x.ID == model.ID));
        }

        internal OrganizationModel GetCharacterById(string id) => Organizations.Find(x => x.ID == id);

        private void EditCharacter(OrganizationModel model, OrganizationModel oldModel)
        {
            Organizations.Remove(oldModel);
            AddCharacter(model);
        }

        private void AddCharacter(OrganizationModel model)
        {
            Organizations.Add(model);
            Repository.Model.Update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteCharacter(OrganizationModel model)
        {
            Organizations.Remove(model);
            Observer.UpdateGeneralObserver();
        }

        internal OrganizationModel GetCopy(OrganizationModel model)
        {
            var result = new OrganizationModel() { ID = model.ID };
            CopyInfo(model, result);
            return result;
        }

        internal OrganizationModel GetDuplicate(OrganizationModel model)
        {
            var result = new OrganizationModel() { ID = GetNewID() };
            CopyInfo(model, result);
            return result;
        }

        private static void CopyInfo(OrganizationModel model, OrganizationModel result)
        {
            result.Name = model.Name;
            result.Concept = model.Concept;
            result.OrganizationType = model.OrganizationType;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        internal List<string> GetAllTypes() => GetAllData(x => x.OrganizationType);

        protected List<string> GetAllData(Func<OrganizationModel, string> getData)
        {
            var result = new List<string>();
            Organizations.ForEach(x => result.Add(getData(x)));
            return result.Distinct().OrderBy(x => x).ToList();
        }

        #endregion
    }
}
