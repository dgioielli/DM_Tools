using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.Components.Core;
using DMTools.View.EditorView.CharacterEditor;
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

namespace DMTools.View.CharacterEditor
{
    /// <summary>
    /// Lógica interna para CharacterEditorView.xaml
    /// </summary>
    public partial class CharacterEditorView : Window
    {
        #region Variables and Properties

        CharacterRepository Repository => CharacterRepository.GetInstance();

        NotesListManager<CharacterModel> m_notesManager;

        CharacterEditorViewModel m_vm;

        #endregion

        #region Constructors

        public CharacterEditorView(CharacterModel model)
        {
            InitializeComponent();
            m_vm = new CharacterEditorViewModel(model);
            m_notesManager = new NotesListManager<CharacterModel>(m_vm);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            m_notesManager.ShowNotes(pnl_notes);
            cbo_race.SetOptions(Repository.GetAllRaces());
            cbo_class.SetOptions(Repository.GetAllClass());
            cbo_clan.SetOptions(Repository.GetAllClans());
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

        public static void Show(CharacterModel characterModel)
        {
            var dlg = new CharacterEditorView(characterModel);
            dlg.Show();
        }

        #endregion
    }
}
