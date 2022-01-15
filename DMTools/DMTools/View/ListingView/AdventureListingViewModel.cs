using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ContentViewer;
using DMTools.View.EditorView.AdventureEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.ListingView
{
    class AdventureListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        AdventureRepository Repository => AdventureRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Adventure";

        #endregion

        #region Constructors

        public AdventureListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => AdventureEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "adventure");

        public override void DuplicateObject(string id) => AdventureEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => AdventureEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBaseFilter(TXT_Filter);

        public override void ShowObject(string id)
        {
            var content = new ContentViewerAdventureModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
