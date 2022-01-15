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
    class OrganizationEditorViewModel : BaseObjectEditorViewModel<OrganizationModel>
    {
        #region Variables and Properties

        OrganizationRepository Repository => OrganizationRepository.GetInstance();
        
        protected override string TypeEditor => "Organization";
        List<ObjectInfoModel> m_characters = new List<ObjectInfoModel>();

        public string TXT_Concept { get => m_model.Concept; set { m_model.Concept = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.OrganizationType; set { m_model.OrganizationType = value; OnPropertyChanged(); } }
        public List<ObjectInfoModel> LST_Characters { get => m_characters; }

        #endregion

        #region Constructors

        public OrganizationEditorViewModel(OrganizationModel model) : base(model)
        {
            m_characters.Clear();
            m_model.Members.ForEach(x => m_characters.Add(new ObjectInfoModel(x)));
        }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Concept));
            UpdateCharacters();
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
            UpdateList(m_model.Members, m_characters, x => x.ObjectId.Trim() == "" && x.Info == "");
            Repository.AddEditObject(m_model);
        }

        #endregion
    }
}
