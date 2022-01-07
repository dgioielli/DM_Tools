using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace DMTools.View.ListingView
{
    public abstract class BaseListingViewModel : DGViewModel, IGeneralObserver
    {
        #region Variables and Properties

        protected FlowDocument m_document = new FlowDocument();

        public abstract string WDW_Title { get; }

        public ICommand BTN_New { get; protected set; }

        #endregion

        #region Constructors

        public BaseListingViewModel()
        { }

        #endregion

        #region Functions

        protected override void assinarComandos()
        {
            BTN_New = new DGCommand(obj => NewObject());
            base.assinarComandos();
        }

        public FlowDocument GetDocument() => m_document;

        public void Update()
        {
            OnPropertyChanged(nameof(GetDocument));
            OnPropertyChanged(nameof(GetObjects));
        }

        public abstract List<IObjectBase> GetObjects();

        protected abstract void NewObject();

        public abstract void EditObject(string id);

        public abstract void DuplicateObject(string id);

        public abstract void ShowObject(string id);

        public abstract void DeleteObject(string id);

        protected void DeleteObject<T>(string id, ObjectBaseRepository<T> repository, string type)
            where T : IObjectBase, new()
        {
            var item = repository.GetObjectById(id);
            if (item == null) return;
            if (MessageBox.Show(MessageKeys.GetDeleteMsg(type, item.Name), "DM Tools", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            repository.DeleteObject(item);
        }

        #endregion
    }
}
