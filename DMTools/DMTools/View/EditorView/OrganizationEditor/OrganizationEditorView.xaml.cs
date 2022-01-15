using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.Components.Core;
using DMTools.View.EditorView.Components;
using DMTools.View.EditorView.Components.NotesObject;
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

namespace DMTools.View.OrganizationEditor
{
    /// <summary>
    /// Lógica interna para OrganizationEditorView.xaml
    /// </summary>
    public partial class OrganizationEditorView : Window
    {
        #region Variables and Properties

        OrganizationRepository Repository => OrganizationRepository.GetInstance();

        NotesListManager<OrganizationModel> m_notesManager;
        CharacterInfoControlManager m_charManager;

        OrganizationEditorViewModel m_vm;

        #endregion

        #region Constructors

        public OrganizationEditorView(OrganizationModel model)
        {
            InitializeComponent();
            m_vm = new OrganizationEditorViewModel(model);
            m_notesManager = new NotesListManager<OrganizationModel>(m_vm);
            m_charManager = new CharacterInfoControlManager(m_vm.Update);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            m_notesManager.ShowNotes(pnl_notes);
            m_charManager.ShowCharacters(pnl_characters, m_vm.LST_Characters);
            cbo_type.SetOptions(Repository.GetAllTypes());
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyEventKeys.Close) Close();
            else if (e.PropertyName == nameof(m_vm.LST_Notes)) m_notesManager.ShowNotes(pnl_notes);
            else if (e.PropertyName == nameof(m_vm.LST_Characters)) m_charManager.ShowCharacters(pnl_characters, m_vm.LST_Characters);
        }

        private void SetActions()
        {
            this.Loaded += (sender, e) => m_vm.Update();
        }

        #endregion

        #region Functions

        public static void Show(OrganizationModel model)
        {
            var dlg = new OrganizationEditorView(model);
            dlg.Show();
        }

        #endregion
    }
}
