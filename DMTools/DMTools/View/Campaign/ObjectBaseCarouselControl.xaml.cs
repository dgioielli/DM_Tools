using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using DMTools.View.ContentViewer;
using DMTools.View.LocationEditor;
using DMTools.View.OrganizationEditor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMTools.View.Campaign
{
    /// <summary>
    /// Interação lógica para ObjectBaseCarouselControl.xam
    /// </summary>
    public partial class ObjectBaseCarouselControl : UserControl
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();
        PoolGeneric<ObjectSettingCellControl, ObjectSettingModel> m_pool;

        ObjectBaseCarouselControlModel m_vm;

        Action<string> EditObject;
        Action<string> DuplicateObject;
        Action<string> ShowObject;
        Action<string> DeleteObject;

        #endregion

        #region Constructors

        public ObjectBaseCarouselControl(Func<List<IObjectBase>> getObjects, Action newObject, Action showListing, Action<string> editObject, Action<string> duplicateObject, Action<string> showObject, Action<string> deleteObject)
        {
            InitializeComponent();
            m_vm = new ObjectBaseCarouselControlModel(getObjects, newObject, showListing);
            EditObject = editObject;
            DuplicateObject = duplicateObject;
            ShowObject = showObject;
            DeleteObject = deleteObject;
            Observer.RegisterGeneralObserver(m_vm);
            m_pool = new PoolGeneric<ObjectSettingCellControl, ObjectSettingModel>(NewObject, RefreshObject);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            ShowObjects(grd_carousel, m_pool, GetObjects(m_vm.LST_Objects));
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.LST_Objects)) ShowObjects(grd_carousel, m_pool, GetObjects(m_vm.LST_Objects));
        }

        #endregion

        #region Functions

        private void SetItemsGrid(Grid grd, List<ObjectSettingCellControl> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int col = i % 5;
                int row = i / 5;
                var uc = list[i];
                Grid.SetColumn(uc, col + 1);
                Grid.SetRow(uc, row);
                grd.Children.Add(uc);
            }
        }

        private void ShowObjects(Grid grd, PoolGeneric<ObjectSettingCellControl, ObjectSettingModel> pool, List<ObjectSettingModel> objs)
        {
            ClearGrid(grd);
            var list = m_pool.GetObjects(objs);
            SetItemsGrid(grd, list);
        }

        private List<ObjectSettingModel> GetObjects(List<IObjectBase> list)
        {
            var result = new List<ObjectSettingModel>();
            list.ForEach(x => result.Add(new ObjectSettingModel(x, id => EditObject(id), id => DeleteObject(id), id => DuplicateObject(id), id => ShowObject(id))));
            return result;
        }

        private ObjectSettingCellControl RefreshObject(ObjectSettingCellControl arg1, ObjectSettingModel arg2)
        { arg1.Update(arg2); return arg1; }

        private ObjectSettingCellControl NewObject(ObjectSettingModel arg) => new ObjectSettingCellControl(arg);

        private void ClearGrid(Grid grd)
        {
            var children = grd.Children;
            var childrenToRemove = new List<UIElement>();
            for (int i = 0; i < children.Count; i++)
            {
                int col = Grid.GetColumn(children[i]);
                if (col == 0 || col >= 6) continue;
                childrenToRemove.Add(children[i]);
            }
            childrenToRemove.ForEach(x => grd.Children.Remove(x));
        }

        #endregion
    }
}
