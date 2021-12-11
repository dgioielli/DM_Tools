using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class SectionRepository
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static SectionRepository m_instance = new SectionRepository();

        public List<SectionModel> Sections { get => Repository.Model.Sections; }

        #endregion

        #region Constructors

        public static SectionRepository GetInstance()
        { if (m_instance == null) m_instance = new SectionRepository(); return m_instance; }

        private SectionRepository()
        { }

        #endregion

        #region Functions

        public SectionModel GetNewSection()
        {
            var result = new SectionModel() { ID = GetNewID() };
            return result;
        }

        private string GetNewID()
        {
            var now = DateTime.Now;
            return $"Section:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        internal void AddEditSection(SectionModel model)
        {
            if (!Sections.Exists(x => x.ID == model.ID)) AddSection(model);
            else EditSection(model, Sections.Find(x => x.ID == model.ID));
        }

        internal SectionModel GetSectionById(string id) => Sections.Find(x => x.ID == id);

        private void EditSection(SectionModel model, SectionModel oldModel)
        {
            Sections.Remove(oldModel);
            AddSection(model);
        }

        private void AddSection(SectionModel model)
        {
            Sections.Add(model);
            Repository.Model.Update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteSection(SectionModel model)
        {
            Sections.Remove(model);
            Observer.UpdateGeneralObserver();
        }

        internal SectionModel GetCopy(SectionModel model)
        {
            var result = new SectionModel() { ID = model.ID };
            CopyInfo(model, result);
            return result;
        }

        internal SectionModel GetDuplicate(SectionModel model)
        {
            var result = new SectionModel() { ID = GetNewID() };
            CopyInfo(model, result);
            return result;
        }

        private static void CopyInfo(SectionModel model, SectionModel result)
        {
            result.SectionName = model.SectionName;
            result.SectionIntro = model.SectionIntro;
        }

        #endregion
    }
}
