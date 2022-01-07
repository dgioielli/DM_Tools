using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.EditorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.OrganizationEditor
{
    class OrganizationEditorViewModel : EditorViewModel
    {
        #region Variables and Properties

        OrganizationRepository Repository => OrganizationRepository.GetInstance();
        OrganizationModel m_model;

        protected override string TypeEditor => "Organization";

        public override string TXT_Name { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_Concept { get => m_model.Concept; set { m_model.Concept = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.OrganizationType; set { m_model.OrganizationType = value; OnPropertyChanged(); } }
        public List<string> LST_Notes { get => m_model.Notes; }

        #endregion

        #region Constructors

        public OrganizationEditorViewModel(OrganizationModel model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(TXT_Concept));
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
