using DMTools.Keys;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DMTools.View.ContentViewer
{
    class ContentViewerEventModel : ContentViewerViewModel
    {
        #region Variables and Properties

        EventRepository Repository => EventRepository.GetInstance();
        SessionRepository SessionRepository => SessionRepository.GetInstance();

        EventModel m_model;

        public string WDW_Title { get => $"DM Tools - Location : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerEventModel(EventModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            AddHeading3(result, $"Date : {m_model.Day} - {m_model.Month} - {m_model.Year}");
            var location = m_model.GetLocation();
            if (location != null) AddHeading3(result, $"Location : {location.Name}");
            if (m_model.EventType != "") AddHeading2(result, $"{m_model.EventType}");
            AddText(result, $"{m_model.Abstract}");
            AddHeading3(result, $"Participants:");
            m_model.Participants.ForEach(x => AddText(result, FlowDocumentService.GetCharacterInfoRuns(x)));
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            return result;
        }

        public override void Update()
        {
            var model = Repository.GetObjectById(m_model.ID);
            m_model = model;
            if (model == null) OnPropertyChanged(PropertyEventKeys.Close);
            else OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
