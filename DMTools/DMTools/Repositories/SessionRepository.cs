using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SessionModels;
using DMTools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class SessionRepository
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static SessionRepository m_instance = new SessionRepository();

        public List<SessionModel> Sessions { get => Repository.Model.Sessions; }

        #endregion

        #region Constructors

        public static SessionRepository GetInstance()
        { if (m_instance == null) m_instance = new SessionRepository(); return m_instance; }

        private SessionRepository()
        { }

        #endregion

        #region Functions

        public SessionModel GetNewSession()
        {
            var result = new SessionModel() { ID = GetNewID() };
            return result;
        }

        private string GetNewID()
        {
            var now = DateTime.Now;
            return $"Session:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        internal void AddEditSession(SessionModel model)
        {
            if (!Sessions.Exists(x => x.ID == model.ID)) AddSession(model);
            else EditSession(model, Sessions.Find(x => x.ID == model.ID));
        }

        internal SessionModel GetSectionById(string id) => Sessions.Find(x => x.ID == id);

        private void EditSession(SessionModel model, SessionModel oldModel)
        {
            Sessions.Remove(oldModel);
            AddSession(model);
        }

        private void AddSession(SessionModel model)
        {
            Sessions.Add(model);
            Repository.Model.Update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteSession(SessionModel model)
        {
            Sessions.Remove(model);
            Observer.UpdateGeneralObserver();
        }

        internal SessionModel GetCopy(SessionModel model)
        {
            var result = new SessionModel() { ID = model.ID };
            CopyInfo(model, result);
            return result;
        }

        internal SessionModel GetDuplicate(SessionModel model)
        {
            var result = new SessionModel() { ID = GetNewID() };
            CopyInfo(model, result);
            return result;
        }

        private static void CopyInfo(SessionModel model, SessionModel result)
        {
            result.SessionName = model.SessionName;
            result.SessionIntro = model.SessionIntro;
            model.Possibilities.ForEach(x => result.Possibilities.Add(new PossibilityModel(x)));
            model.Characters.ForEach(x => result.Characters.Add(new SessionCharacterModel(x)));
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        #endregion
    }
}
