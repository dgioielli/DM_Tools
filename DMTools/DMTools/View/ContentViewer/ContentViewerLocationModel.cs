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
    class ContentViewerLocationModel : ContentViewerViewModel
    {
        #region Variables and Properties

        LocationRepository Repository => LocationRepository.GetInstance();
        SessionRepository SessionRepository => SessionRepository.GetInstance();
        EventRepository EventRepository => EventRepository.GetInstance();

        LocationModel m_model;

        public string WDW_Title { get => $"DM Tools - Location : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerLocationModel(LocationModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            AddHeading2(result, $"{m_model.Concept}");
            if (m_model.LocationType != "") AddHeading2(result, $"{m_model.LocationType}");
            AddText(result, $"{m_model.Description}");
            AddHeading3(result, $"Notable Characters:");
            m_model.NotableCharacters.ForEach(x => AddText(result, FlowDocumentService.GetCharacterInfoRuns(x)));
            AddHeading2(result, $"Events:");
            AddList(result, GetEventsLocatedHere());
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            return result;
        }

        private List<string> GetEventsLocatedHere()
        {
            var result = new List<string>();
            EventRepository.Objects.ForEach(x => { if (x.LocationID == m_model.ID) result.Add(x.ShowName); });
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
