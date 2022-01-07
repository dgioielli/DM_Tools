using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using DMTools.View.ContentViewer;
using DMTools.View.EditorView.EventEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DMTools.View.ListingView
{
    class CharacterListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        CharacterRepository Repository => CharacterRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Characters";

        #endregion

        #region Constructors

        public CharacterListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => CharacterEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "character");

        public override void DuplicateObject(string id) => CharacterEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => CharacterEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBase();

        public override void ShowObject(string id)
        {
            var content = new ContentViewerCharacterModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion

    }
}
