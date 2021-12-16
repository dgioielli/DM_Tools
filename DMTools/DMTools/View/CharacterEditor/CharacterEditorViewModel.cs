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

namespace DMTools.View.CharacterEditor
{
    class CharacterEditorViewModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CharacterRepository Repository => CharacterRepository.GetInstance();
        CharacterModel m_model;

        public string TXT_CharacterName { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_CharacterConcept { get => m_model.Concept; set { m_model.Concept = value; OnPropertyChanged(); } }
        public string TXT_CharacterRace { get => m_model.Race; set { m_model.Race = value; OnPropertyChanged(); } }
        public string TXT_CharacterClass { get => m_model.Class; set { m_model.Class = value; OnPropertyChanged(); } }
        public string WDW_Title => $"DM Tools - Section : {m_model.Name}";
        public List<string> LST_Notes { get => m_model.Notes; }

        public ICommand BTN_Update { get; protected set; }
        public ICommand BTN_Conclude { get; protected set; }
        public ICommand BTN_Cancel { get; protected set; }

        #endregion

        #region Constructors

        public CharacterEditorViewModel(CharacterModel model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        public void Update()
        {
            OnPropertyChanged(nameof(TXT_CharacterName));
            OnPropertyChanged(nameof(TXT_CharacterConcept));
            OnPropertyChanged(nameof(LST_Notes));
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
            Repository.AddEditSection(m_model);
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
