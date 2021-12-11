using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.SectionEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace DMTools.View.ContentViewer
{
    public abstract class ContentViewerViewModel : DGViewModel, IGeneralObserver
    {
        #region Variables and Properties

        #endregion

        #region Constructors

        public ContentViewerViewModel()
        { }

        #endregion

        #region Functions

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        public abstract FlowDocument GetDocument();

        public abstract void Update();

        #endregion
    }
}
