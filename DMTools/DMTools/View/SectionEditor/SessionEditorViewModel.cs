using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SessionModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.SectionEditor
{
    class SessionEditorViewModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        SessionRepository Repository => SessionRepository.GetInstance();
        CharacterRepository CharRepository => CharacterRepository.GetInstance();
        SessionModel m_model;

        List<PossibilityModel> m_possibilities = new List<PossibilityModel>();
        List<SessionCharacterModel> m_characters = new List<SessionCharacterModel>();

        public string TXT_SectionName { get => m_model.SessionName; set { m_model.SessionName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_SectionIntro { get => m_model.SessionIntro; set { m_model.SessionIntro = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string WDW_Title { get => $"DM Tools - Section : {m_model.SessionName}"; }
        public List<string> LST_Notes { get => m_model.Notes; }
        public List<PossibilityModel> LST_Possibilities { get => m_possibilities; }
        public List<SessionCharacterModel> LST_Characters { get => m_characters; }

        public ICommand BTN_Update { get; protected set; }
        public ICommand BTN_Conclude { get; protected set; }
        public ICommand BTN_Cancel { get; protected set; }

        #endregion

        #region Constructors

        public SessionEditorViewModel(SessionModel model)
        {
            m_model = model;
            m_possibilities.Clear();
            m_model.Possibilities.ForEach(x => m_possibilities.Add(new PossibilityModel(x)));
            m_characters.Clear();
            m_model.Characters.ForEach(x => m_characters.Add(new SessionCharacterModel(x)));
        }

        #endregion

        #region Functions

        public void Update()
        {
            UpdatePossibilities();
            UpdateCharacters();
            OnPropertyChanged(nameof(TXT_SectionName));
            OnPropertyChanged(nameof(TXT_SectionIntro));
            OnPropertyChanged(nameof(LST_Notes));
            OnPropertyChanged(nameof(LST_Possibilities));
            OnPropertyChanged(nameof(LST_Characters));
        }

        private void UpdatePossibilities()
        {
            ClearList(m_possibilities, x => x.Text.Trim() == "");
            m_possibilities.Add(new PossibilityModel());
        }
        
        private void ClearList<T>(List<T> list, Func<T, bool> checkFunc)
        {
            var empties = list.FindAll(x => checkFunc(x));
            empties.ForEach(x => list.Remove(x));
        }

        private void UpdateCharacters()
        {
            ClearList(m_characters, x => x.CharacterId.Trim() == "" && x.Info == "");
            m_characters.Add(new SessionCharacterModel());
        }

        protected override void assinarComandos()
        {
            BTN_Update = new DGCommand(obj => UpdateSection());
            BTN_Conclude = new DGCommand(obj => { UpdateSection(); OnPropertyChanged(PropertyEventKeys.Close); });
            BTN_Cancel = new DGCommand(obj => OnPropertyChanged(PropertyEventKeys.Close));
            base.assinarComandos();
        }

        private void UpdateSection()
        {
            m_characters = m_characters.OrderBy(x => x.Role).ThenBy(x => GetCharName(x.CharacterId)).ToList();
            UpdateList(m_model.Possibilities, m_possibilities, x => x.Text.Trim() == "");
            UpdateList(m_model.Characters, m_characters, x => x.CharacterId.Trim() == "" && x.Info == "");
            Repository.AddEditSession(m_model);
        }

        private string GetCharName(string characterId)
        {
            var c = CharRepository.GetObjectById(characterId);
            if (c == null) return "";
            return c.Name;
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
