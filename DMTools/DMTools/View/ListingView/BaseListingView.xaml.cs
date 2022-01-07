using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.View.Campaign;
using DMTools.View.ContentViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DMTools.View.ListingView
{
    /// <summary>
    /// Lógica interna para BaseListingView.xaml
    /// </summary>
    public partial class BaseListingView : Window
    {
        #region Variables and Properties

        protected ObserverManager Observer => ObserverManager.GetInstance();
        PoolGeneric<ObjectBaseCellControl, ObjectSettingModel> m_pool;

        BaseListingViewModel m_vm;

        #endregion

        #region Constructors

        public BaseListingView(BaseListingViewModel vm)
        {
            InitializeComponent();
            m_vm = vm;
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            m_pool = new PoolGeneric<ObjectBaseCellControl, ObjectSettingModel>(NewObject, RefreshObject);
            Observer.RegisterGeneralObserver(m_vm);
            SetActions();
            ShowDocument();
            ShowList();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.GetDocument)) ShowDocument();
            else if (e.PropertyName == nameof(m_vm.GetObjects)) ShowList();
            else if (e.PropertyName == PropertyEventKeys.Close) Close();
        }

        private void SetActions()
        {
            this.Closing += (sender, e) => Observer.QueueUnregisterGeneralObserver(m_vm);
        }

        #endregion

        #region Functions

        private void ShowDocument()
        {
            txt_content.Document = m_vm.GetDocument();
        }

        private void ShowList()
        {
            pnl_items.Children.Clear();
            var list = m_pool.GetObjects(GetObjects(m_vm.GetObjects()));
            list.ForEach(x => pnl_items.Children.Add(x));
        }

        private List<ObjectSettingModel> GetObjects(List<IObjectBase> list)
        {
            var result = new List<ObjectSettingModel>();
            list.ForEach(x => result.Add(new ObjectSettingModel(x, id => m_vm.EditObject(id), id => m_vm.DeleteObject(id), id => m_vm.DuplicateObject(id), id => m_vm.ShowObject(id))));
            return result;
        }

        private ObjectBaseCellControl RefreshObject(ObjectBaseCellControl arg1, ObjectSettingModel arg2)
        { arg1.Update(arg2); return arg1; }

        private ObjectBaseCellControl NewObject(ObjectSettingModel arg) => new ObjectBaseCellControl(arg);

        public static void Show(BaseListingViewModel viewModel)
        {
            var dlg = new BaseListingView(viewModel);
            dlg.Show();
        }

        #endregion
    }
}
