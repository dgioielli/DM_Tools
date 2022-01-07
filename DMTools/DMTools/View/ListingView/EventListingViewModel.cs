using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ContentViewer;
using DMTools.View.EditorView.EventEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.ListingView
{
    class EventListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        EventRepository Repository => EventRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Events";

        #endregion

        #region Constructors

        public EventListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => EventEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "character");

        public override void DuplicateObject(string id) => EventEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => EventEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBase();

        public override void ShowObject(string id)
        {
            var content = new ContentViewerEventModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
