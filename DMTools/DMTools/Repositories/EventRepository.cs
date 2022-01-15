using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class EventRepository : ObjectBaseRepository<EventModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static EventRepository m_instance = new EventRepository();

        public override List<EventModel> Objects => Repository.Model.Setting.Events;

        #endregion

        #region Constructors

        public static EventRepository GetInstance()
        { if (m_instance == null) m_instance = new EventRepository(); return m_instance; }

        private EventRepository()
        { m_update = Repository.Model.Setting.UpdateEvents; }

        #endregion

        #region Functions

        protected override void CopyInfo(EventModel model, EventModel result)
        {
            result.Abstract = model.Abstract;
            result.Day = model.Day;
            result.LocationID = model.LocationID;
            result.EventType = model.EventType;
            result.Month = model.Month;
            result.Name = model.Name;
            model.Notes.ForEach(x => result.Notes.Add(x));
            model.Participants.ForEach(x => result.Participants.Add(new ObjectInfoModel(x)));
            result.Year = model.Year;
        }

        public List<string> GetAllTypes() => GetAllData(x => x.EventType);

        #endregion
    }
}
