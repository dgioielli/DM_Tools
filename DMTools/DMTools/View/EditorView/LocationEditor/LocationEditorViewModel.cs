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

namespace DMTools.View.LocationEditor
{
    class LocationEditorViewModel : BaseObjectEditorViewModel<LocationModel>
    {
        #region Variables and Properties

        LocationRepository Repository => LocationRepository.GetInstance();

        protected override string TypeEditor => "Location";
        List<ObjectInfoModel> m_characters = new List<ObjectInfoModel>();

        public string TXT_Concept { get => m_model.Concept; set { m_model.Concept = value; OnPropertyChanged(); } }
        public string TXT_Type { get => m_model.LocationType; set { m_model.LocationType = value; OnPropertyChanged(); } }
        public string TXT_Description { get => m_model.Description; set { m_model.Description = value; OnPropertyChanged(); } }
        public List<ObjectInfoModel> LST_Characters { get => m_characters; }

        #endregion

        #region Constructors

        public LocationEditorViewModel(LocationModel model) : base(model)
        {
            m_characters.Clear();
            m_model.NotableCharacters.ForEach(x => m_characters.Add(new ObjectInfoModel(x)));
        }

        #endregion

        #region Functions

        public override void Update()
        {
            UpdateCharacters();
            OnPropertyChanged(nameof(TXT_Concept));
            OnPropertyChanged(nameof(TXT_Description));
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
            UpdateList(m_model.NotableCharacters, m_characters, x => x.ObjectId.Trim() == "" && x.Info == ""); 
            Repository.AddEditObject(m_model);
        }

        #endregion
    }
}
