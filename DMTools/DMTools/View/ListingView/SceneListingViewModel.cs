using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.ContentViewer;
using DMTools.View.EditorView.SceneEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.ListingView
{
    class SceneListingViewModel : BaseListingViewModel
    {
        #region Variables and Properties

        SceneRepository Repository => SceneRepository.GetInstance();

        public override string WDW_Title => $"DM Tools - Scene";

        #endregion

        #region Constructors

        public SceneListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void NewObject() => SceneEditorView.Show(Repository.GetNewObject());

        public override void DeleteObject(string id) => DeleteObject(id, Repository, "scene");

        public override void DuplicateObject(string id) => SceneEditorView.Show(Repository.GetDuplicate(Repository.GetObjectById(id)));

        public override void EditObject(string id) => SceneEditorView.Show(Repository.GetCopy(Repository.GetObjectById(id)));

        public override List<IObjectBase> GetObjects() => Repository.GetAllObjectsBase();

        public override void ShowObject(string id)
        {
            var content = new ContentViewerSceneModel(Repository.GetObjectById(id));
            m_document = content.GetDocument();
            OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
