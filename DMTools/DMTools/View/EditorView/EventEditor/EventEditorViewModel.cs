using DMTools.CoreLib;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView.EventEditor
{
    class EventEditorViewModel : BaseObjectEditorViewModel<EventModel>
    {
        #region Variables and Properties

        EventRepository Repository => EventRepository.GetInstance();
        protected string m_location = "";
        List<ObjectInfoModel> m_characters = new List<ObjectInfoModel>();

        protected override string TypeEditor => "Event";

        public string TXT_Abstract { get => m_model.Abstract; set { m_model.Abstract = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.EventType; set { m_model.EventType = value; OnPropertyChanged(); } }
        public string TXT_Day { get => m_model.Day.ToString("00"); set { m_model.Day = value.ToInt(); OnPropertyChanged(); } }
        public string TXT_Month { get => m_model.Month.ToString("00"); set { m_model.Month = value.ToInt(); OnPropertyChanged(); } }
        public string TXT_Year { get => m_model.Year.ToString("000"); set { m_model.Year = value.ToInt(); OnPropertyChanged(); } }
        public string TXT_Location { get => m_location; set { m_location = value; OnPropertyChanged(); } }
        public List<ObjectInfoModel> LST_Characters { get => m_characters; }

        #endregion

        #region Constructors

        public EventEditorViewModel(EventModel model) : base(model)
        {
            var loc = m_model.GetLocation();
            if (loc == null) TXT_Location = null;
            else TXT_Location = loc.ShowName;
            m_characters.Clear();
            m_model.Participants.ForEach(x => m_characters.Add(new ObjectInfoModel(x)));
        }

        #endregion

        #region Functions

        public override void Update()
        {
            UpdateCharacters();
            OnPropertyChanged(nameof(TXT_Abstract));
            OnPropertyChanged(nameof(TXT_Day));
            OnPropertyChanged(nameof(TXT_Month));
            OnPropertyChanged(nameof(TXT_Year));
            OnPropertyChanged(nameof(TXT_Location));
            OnPropertyChanged(nameof(LST_Characters));
            base.Update();
        }

        private void UpdateCharacters()
        {
            ClearList(m_characters, x => x.ObjectId.Trim() == "" && x.Info == "");
            m_characters.Add(new ObjectInfoModel());
        }

        protected override void UpdateObject()
        {
            m_model.SetLocation(m_location);
            UpdateList(m_model.Participants, m_characters, x => x.ObjectId.Trim() == "" && x.Info == "");
            Repository.AddEditObject(m_model);
        }

        #endregion
    }
}
