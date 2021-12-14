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

        public string TXT_CharacterName { get => m_model.CharacterName; set { m_model.CharacterName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_CharacterConcept { get => m_model.CharacterConcept; set { m_model.CharacterConcept = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string WDW_Title => $"DM Tools - Section : {m_model.CharacterName}";
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
