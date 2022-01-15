using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Models.CampaignModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.Components.Core;
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

namespace DMTools.View.EditorView.AdventureEditor
{
    /// <summary>
    /// Lógica interna para AdventureEditorView.xaml
    /// </summary>
    public partial class AdventureEditorView : Window
    {
        #region Variables and Properties

        AdventureRepository Repository => AdventureRepository.GetInstance();

        NotesListManager<AdventureModel> m_notesManager;

        AdventureEditorViewModel m_vm;

        #endregion

        #region Constructors

        public AdventureEditorView(AdventureModel model)
        {
            InitializeComponent();
            m_vm = new AdventureEditorViewModel(model);
            m_notesManager = new NotesListManager<AdventureModel>(m_vm);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            m_notesManager.ShowNotes(pnl_notes);
            cbo_type.SetOptions(Repository.GetAllTypes());
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyEventKeys.Close) Close();
            else if (e.PropertyName == nameof(m_vm.LST_Notes)) m_notesManager.ShowNotes(pnl_notes);
        }

        private void SetActions()
        {
            this.Loaded += (sender, e) => m_vm.Update();
        }

        #endregion

        #region Functions

        public static void Show(AdventureModel model)
        {
            var dlg = new AdventureEditorView(model);
            dlg.Show();
        }

        #endregion
    }
}
