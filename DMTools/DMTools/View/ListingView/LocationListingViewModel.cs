using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ContentViewer;
using DMTools.View.LocationEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.ListingView
{
    class LocationListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        LocationRepository Repository => LocationRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Locations";

        #endregion

        #region Constructors

        public LocationListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => LocationEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "location");

        public override void DuplicateObject(string id) => LocationEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => LocationEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBase();

        public override void ShowObject(string id)
        {
            var content = new ContentViewerLocationModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
