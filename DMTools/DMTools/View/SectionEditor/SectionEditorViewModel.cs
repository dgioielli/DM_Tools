using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SectionModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.SectionEditor
{
    class SectionEditorViewModel : DGViewModel
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        SectionRepository Repository => SectionRepository.GetInstance();
        SectionModel m_model;

        List<PossibilityModel> m_possibilities = new List<PossibilityModel>();

        public string TXT_SectionName { get => m_model.SectionName; set { m_model.SectionName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_SectionIntro { get => m_model.SectionIntro; set { m_model.SectionIntro = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string WDW_Title { get => $"DM Tools - Section : {m_model.SectionName}"; }
        public List<string> LST_Notes { get => m_model.Notes; }
        public List<PossibilityModel> LST_Possibilities { get => m_possibilities; }

        public ICommand BTN_Update { get; protected set; }
        public ICommand BTN_Conclude { get; protected set; }
        public ICommand BTN_Cancel { get; protected set; }

        #endregion

        #region Constructors

        public SectionEditorViewModel(SectionModel model)
        {
            m_model = model;
            m_possibilities.Clear();
            m_model.Possibilities.ForEach(x => m_possibilities.Add(new PossibilityModel(x)));
        }

        #endregion

        #region Functions

        public void Update()
        {
            UpdatePossibilities();
            OnPropertyChanged(nameof(TXT_SectionName));
            OnPropertyChanged(nameof(TXT_SectionIntro));
            OnPropertyChanged(nameof(LST_Notes));
            OnPropertyChanged(nameof(LST_Possibilities));
        }

        private void UpdatePossibilities()
        {
            var empties = m_possibilities.FindAll(x => x.Text.Trim() == "");
            empties.ForEach(x => m_possibilities.Remove(x));
            m_possibilities.Add(new PossibilityModel());
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
            m_model.Possibilities.Clear();
            var empties = m_possibilities.FindAll(x => x.Text.Trim() == "");
            empties.ForEach(x => m_possibilities.Remove(x));
            m_model.Possibilities.AddRange(m_possibilities);
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
