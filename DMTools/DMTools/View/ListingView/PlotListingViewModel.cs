using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ContentViewer;
using DMTools.View.EditorView.PlotEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.ListingView
{
    class PlotListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        PlotRepository Repository => PlotRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Plot";

        #endregion

        #region Constructors

        public PlotListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => PlotEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "plot");

        public override void DuplicateObject(string id) => PlotEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => PlotEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBase();

        public override void ShowObject(string id)
        {
            var content = new ContentViewerPlotModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
