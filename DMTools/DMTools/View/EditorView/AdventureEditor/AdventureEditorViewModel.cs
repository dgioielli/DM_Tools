using DMTools.Models.CampaignModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView.AdventureEditor
{
    class AdventureEditorViewModel : EditorViewModel
    {
        #region Variables and Properties

        AdventureRepository Repository => AdventureRepository.GetInstance();
        AdventureModel m_model;

        protected override string TypeEditor => "Adventure";

        public override string TXT_Name { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public string TXT_Abstract { get => m_model.Abstract; set { m_model.Abstract = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.AdventureType; set { m_model.AdventureType = value; OnPropertyChanged(); } }
        public List<string> LST_Notes { get => m_model.Notes; }

        #endregion

        #region Constructors

        public AdventureEditorViewModel(AdventureModel model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(TXT_Abstract));
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
