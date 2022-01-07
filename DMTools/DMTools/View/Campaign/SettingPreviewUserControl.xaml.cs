using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using DMTools.View.ContentViewer;
using DMTools.View.EditorView.EventEditor;
using DMTools.View.ListingView;
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
    /// Interação lógica para SettingPreviewUserControl.xam
    /// </summary>
    public partial class SettingPreviewUserControl : UserControl
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();
        CharacterRepository CharRepository => CharacterRepository.GetInstance();
        OrganizationRepository OrgRepository => OrganizationRepository.GetInstance();
        LocationRepository LocRepository => LocationRepository.GetInstance();
        EventRepository EventRepository => EventRepository.GetInstance();

        SettingPreviewUserControlModel m_vm = new SettingPreviewUserControlModel();

        #endregion

        #region Constructors

        public SettingPreviewUserControl()
        {
            InitializeComponent();
            Observer.RegisterGeneralObserver(m_vm);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            SetObjectsCarousels();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.TXT_VisibilitySettingName)) SetTxtSettingNameFocus();
        }

        private void SetActions()
        {
            grd_SettingName.MouseEnter += (sender, e) => btn_editSettingName.Visibility = Visibility.Visible;
            grd_SettingName.MouseLeave += (sender, e) => btn_editSettingName.Visibility = Visibility.Collapsed;
            txt_settingName.LostFocus += (sender, e) => m_vm.TXT_VisibilitySettingName = Visibility.Hidden;
        }

        #endregion

        #region Functions

        private void SetObjectsCarousels()
        {
            var uc = new ObjectBaseCarouselControl(
                CharRepository.GetAllObjectsBase, 
                () => CharacterEditorView.Show(CharRepository.GetNewObject()),
                () => BaseListingView.Show(new CharacterListingViewModel()),
                id => CharacterEditorView.Show(CharRepository.GetCopy(CharRepository.GetObjectById(id))),
                id => CharacterEditorView.Show(CharRepository.GetDuplicate(CharRepository.GetObjectById(id))),
                id => ContentViewerView.Show(new ContentViewerCharacterModel(CharRepository.GetObjectById(id))),
                id => DeleteObject(id, CharRepository, "character"));
            pnl_characters.Children.Add(uc);

            uc = new ObjectBaseCarouselControl(
                LocRepository.GetAllObjectsBase,
                () => LocationEditorView.Show(LocRepository.GetNewObject()),
                () => BaseListingView.Show(new LocationListingViewModel()),
                id => LocationEditorView.Show(LocRepository.GetCopy(LocRepository.GetObjectById(id))),
                id => LocationEditorView.Show(LocRepository.GetDuplicate(LocRepository.GetObjectById(id))),
                id => ContentViewerView.Show(new ContentViewerLocationModel(LocRepository.GetObjectById(id))),
                id => DeleteObject(id, LocRepository, "location"));
            pnl_locations.Children.Add(uc);
            
            uc = new ObjectBaseCarouselControl(
                OrgRepository.GetAllObjectsBase,
                () => OrganizationEditorView.Show(OrgRepository.GetNewObject()),
                () => BaseListingView.Show(new OrganizationListingViewModel()),
                id => OrganizationEditorView.Show(OrgRepository.GetCopy(OrgRepository.GetObjectById(id))),
                id => OrganizationEditorView.Show(OrgRepository.GetDuplicate(OrgRepository.GetObjectById(id))),
                id => ContentViewerView.Show(new ContentViewerOrganizationModel(OrgRepository.GetObjectById(id))),
                id => DeleteObject(id, OrgRepository, "organization"));
            pnl_organizations.Children.Add(uc);

            uc = new ObjectBaseCarouselControl(
                EventRepository.GetAllObjectsBase,
                () => EventEditorView.Show(EventRepository.GetNewObject()),
                () => BaseListingView.Show(new EventListingViewModel()),
                id => EventEditorView.Show(EventRepository.GetCopy(EventRepository.GetObjectById(id))),
                id => EventEditorView.Show(EventRepository.GetDuplicate(EventRepository.GetObjectById(id))),
                id => ContentViewerView.Show(new ContentViewerEventModel(EventRepository.GetObjectById(id))),
                id => DeleteObject(id, EventRepository, "event"));
            pnl_events.Children.Add(uc);
        }

        private void SetTxtSettingNameFocus()
        {
            txt_settingName.Visibility = m_vm.TXT_VisibilitySettingName;
            if (m_vm.TXT_VisibilitySettingName != Visibility.Visible) return;
            txt_settingName.Focus();
            txt_settingName.SelectAll();
        }

        private void DeleteObject<T>(string id, ObjectBaseRepository<T> repository, string type)
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
