using DMTools.Keys;
using DMTools.Models.SessionModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DMTools.View.ContentViewer
{
    class ContentViewerCharacterModel : ContentViewerViewModel
    {
        #region Variables and Properties

        CharacterRepository Repository => CharacterRepository.GetInstance();
        SessionRepository SessionRepository => SessionRepository.GetInstance();
        EventRepository EventRepository => EventRepository.GetInstance();

        CharacterModel m_model;

        public string WDW_Title { get => $"DM Tools - Character : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerCharacterModel(CharacterModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            AddHeading2(result, $"{m_model.Concept}");
            if (m_model.Race != "" && m_model.Class != "") AddHeading2(result, $"{m_model.Race} - {m_model.Class}");
            else if (m_model.Race != "") AddHeading2(result, $"{m_model.Race}");
            else if (m_model.Class != "") AddHeading2(result, $"{m_model.Class}");
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            AddHeading2(result, $"Events:");
            AddList(result, GetEvents());
            AddHeading2(result, $"Session mentions:");
            AddList(result, GetSessionMentions());
            return result;
        }

        private List<string> GetEvents()
        {
            var result = new List<string>();

            EventRepository.Objects.ForEach(x => x.Participants.ForEach(c => { if (c.CharacterId == m_model.ID) result.Add(x.ShowName); }));
            result = result.Distinct().ToList();

            return result;
        }

        private List<string> GetSessionMentions()
        {
            var result = new List<string>();

            var list = new List<Tuple<SessionCharacterModel, string>>();
            SessionRepository.Sessions.ForEach(x => x.Characters.ForEach(c => { if (c.CharacterId == m_model.ID) list.Add(new Tuple<SessionCharacterModel, string>(c, x.SessionName)); }));
            for (int i = 0; i < list.Count; i++)
            {
                var duplicated = list.FindAll(x => x != list[i] && x.Item1.Info == list[i].Item1.Info);
                var sessions = $"{list[i].Item2}";
                duplicated.ForEach(x => sessions = $"{sessions}, {x.Item2}");
                duplicated.ForEach(x => list.Remove(x));
                result.Add($"{sessions} - {list[i].Item1.Info}");
            }

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
