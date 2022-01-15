using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ContentViewer;
using DMTools.View.OrganizationEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.ListingView
{
    class OrganizationListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        OrganizationRepository Repository => OrganizationRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Organizations";

        #endregion

        #region Constructors

        public OrganizationListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => OrganizationEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "organization");

        public override void DuplicateObject(string id) => OrganizationEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => OrganizationEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBaseFilter(TXT_Filter);

        public override void ShowObject(string id)
        {
            var content = new ContentViewerOrganizationModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
