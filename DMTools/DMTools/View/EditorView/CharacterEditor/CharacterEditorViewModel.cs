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
    class CharacterEditorViewModel : BaseObjectEditorViewModel<CharacterModel>
    {
        #region Variables and Properties

        CharacterRepository Repository => CharacterRepository.GetInstance();

        protected override string TypeEditor => "Character";

        public string TXT_CharacterConcept { get => m_model.Concept; set { m_model.Concept = value; OnPropertyChanged(); } }
        public string TXT_CharacterRace { get => m_model.Race; set { m_model.Race = value; OnPropertyChanged(); } }
        public string TXT_CharacterClass { get => m_model.Class; set { m_model.Class = value; OnPropertyChanged(); } }
        public string TXT_CharacterClan { get => m_model.Clan; set { m_model.Clan = value; OnPropertyChanged(); } }
        
        #endregion

        #region Constructors

        public CharacterEditorViewModel(CharacterModel model) : base(model)
        { }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_CharacterConcept));
            base.Update();
        }

        protected override void UpdateObject() => Repository.AddEditObject(m_model);

        #endregion
    }
}
