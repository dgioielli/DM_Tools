using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.EditorView.CharacterEditor
{
    class CharacterEditorViewModel : EditorViewModel
    {
        #region Variables and Properties

        CharacterRepository Repository => CharacterRepository.GetInstance();
        CharacterModel m_model;

        protected override string TypeEditor => "Character";

        public override string TXT_Name { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_CharacterConcept { get => m_model.Concept; set { m_model.Concept = value; OnPropertyChanged(); } }
        public string TXT_CharacterRace { get => m_model.Race; set { m_model.Race = value; OnPropertyChanged(); } }
        public string TXT_CharacterClass { get => m_model.Class; set { m_model.Class = value; OnPropertyChanged(); } }
        public string TXT_CharacterClan { get => m_model.Clan; set { m_model.Clan = value; OnPropertyChanged(); } }
        public List<string> LST_Notes { get => m_model.Notes; }

        #endregion

        #region Constructors

        public CharacterEditorViewModel(CharacterModel model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(TXT_CharacterConcept));
            OnPropertyChanged(nameof(LST_Notes));
        }

        protected override void UpdateObject() => Repository.AddEditObject(m_model);

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
