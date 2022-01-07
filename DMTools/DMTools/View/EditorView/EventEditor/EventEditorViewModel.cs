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
    class EventEditorViewModel : EditorViewModel
    {
        #region Variables and Properties

        EventRepository Repository => EventRepository.GetInstance();
        EventModel m_model;
        protected string m_location = "";
        List<CharacterEventModel> m_characters = new List<CharacterEventModel>();

        protected override string TypeEditor => "Event";

        public override string TXT_Name { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_Abstract { get => m_model.Abstract; set { m_model.Abstract = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.EventType; set { m_model.EventType = value; OnPropertyChanged(); } }
        public string TXT_Day { get => m_model.Day.ToString("00"); set { m_model.Day = value.ToInt(); OnPropertyChanged(); } }
        public string TXT_Month { get => m_model.Month.ToString("00"); set { m_model.Month = value.ToInt(); OnPropertyChanged(); } }
        public string TXT_Year { get => m_model.Year.ToString("000"); set { m_model.Year = value.ToInt(); OnPropertyChanged(); } }
        public string TXT_Location { get => m_location; set { m_location = value; OnPropertyChanged(); } }
        public List<string> LST_Notes { get => m_model.Notes; }
        public List<CharacterEventModel> LST_Characters { get => m_characters; }

        #endregion

        #region Constructors

        public EventEditorViewModel(EventModel model)
        {
            m_model = model;
            var loc = m_model.GetLocation();
            if (loc == null) TXT_Location = null;
            else TXT_Location = loc.ShowName;
            m_characters.Clear();
            m_model.Participants.ForEach(x => m_characters.Add(new CharacterEventModel(x)));
        }

        #endregion

        #region Functions

        public override void Update()
        {
            UpdateCharacters();
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(TXT_Abstract));
            OnPropertyChanged(nameof(TXT_Day));
            OnPropertyChanged(nameof(TXT_Month));
            OnPropertyChanged(nameof(TXT_Year));
            OnPropertyChanged(nameof(TXT_Location));
            OnPropertyChanged(nameof(LST_Notes));
            OnPropertyChanged(nameof(LST_Characters));
        }

        private void ClearList<T>(List<T> list, Func<T, bool> checkFunc)
        {
            var empties = list.FindAll(x => checkFunc(x));
            empties.ForEach(x => list.Remove(x));
        }

        private void UpdateCharacters()
        {
            ClearList(m_characters, x => x.CharacterId.Trim() == "" && x.Info == "");
            m_characters.Add(new CharacterEventModel());
        }

        protected override void UpdateObject()
        {
            m_model.SetLocation(m_location);
            UpdateList(m_model.Participants, m_characters, x => x.CharacterId.Trim() == "" && x.Info == "");
            Repository.AddEditObject(m_model);
        }

        private void UpdateList<T>(List<T> originalList, List<T> currentList, Func<T, bool> checkFunc)
        {
            originalList.Clear();
            ClearList(currentList, checkFunc);
            originalList.AddRange(currentList);
        }

        public void SetNotes(List<string> notes)
        {
            m_model.Notes.Clear();
            foreach (var note in notes)
                if (note != "") m_model.Notes.Add(note);
            OnPropertyChanged(nameof(LST_Notes));
        }

        #endregion
    }
}
