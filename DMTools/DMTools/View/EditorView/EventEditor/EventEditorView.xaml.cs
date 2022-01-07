using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.Components.Core;
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

namespace DMTools.View.EditorView.EventEditor
{
    /// <summary>
    /// Lógica interna para EventEditorView.xaml
    /// </summary>
    public partial class EventEditorView : Window
    {
        #region Variables and Properties

        EventRepository Repository => EventRepository.GetInstance();

        LocationRepository LocRepository => LocationRepository.GetInstance();

        PoolGeneric<EditableTextBlock, string> m_poolNotes;
        PoolGeneric<EventCharacterCellControl, CharacterEventModel> m_poolCharacters;
        List<EditableTextBlock> m_notes = new List<EditableTextBlock>();

        EventEditorViewModel m_vm;

        #endregion

        #region Constructors

        public EventEditorView(EventModel model)
        {
            InitializeComponent();
            m_poolNotes = new PoolGeneric<EditableTextBlock, string>(NewNote, RefreshNote);
            m_poolCharacters = new PoolGeneric<EventCharacterCellControl, CharacterEventModel>(NewCharacter, RefreshCharacter);
            m_vm = new EventEditorViewModel(model);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            ShowNotes();
            ShowCharacters();
            cbo_type.SetOptions(Repository.GetAllTypes());
            cbo_location.SetOptions(LocRepository.GetAllShowNames());
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyEventKeys.Close) Close();
            else if (e.PropertyName == nameof(m_vm.LST_Notes)) ShowNotes();
            else if (e.PropertyName == nameof(m_vm.LST_Characters)) ShowCharacters();
        }

        private void SetActions()
        {
            this.Loaded += (sender, e) => m_vm.Update();
        }

        #endregion

        #region Functions

        private void ShowNotes()
        {
            var list = new List<string>(m_vm.LST_Notes);
            list.Add("");
            m_notes = m_poolNotes.GetObjects(list);
            pnl_notes.Children.Clear();
            m_notes.ForEach(x => pnl_notes.Children.Add(x));
        }

        private EditableTextBlock RefreshNote(EditableTextBlock ed, string text)
        {
            ed.Text = text;
            return ed;
        }

        private EditableTextBlock NewNote(string text)
        {
            var ed = new EditableTextBlock() { AcceptReturn = true, PlaceHolder = "+ + + New Note + + +", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(5), Focusable = true };
            ed.Text = text;
            ed.OnTextChanged += OnNoteChanged;
            return ed;
        }

        private void OnNoteChanged()
        {
            var notes = new List<string>();
            m_notes.ForEach(x => notes.Add(x.TextBase));
            m_vm.SetNotes(notes);
        }

        public static void Show(EventModel characterModel)
        {
            var dlg = new EventEditorView(characterModel);
            dlg.Show();
        }

        private void ShowCharacters()
        {
            var list = m_poolCharacters.GetObjects(m_vm.LST_Characters);
            pnl_characters.Children.Clear();
            list.ForEach(x => pnl_characters.Children.Add(x));
        }

        private EventCharacterCellControl NewCharacter(CharacterEventModel arg) => new EventCharacterCellControl(arg, m_vm.Update);

        private EventCharacterCellControl RefreshCharacter(EventCharacterCellControl arg1, CharacterEventModel arg2)
        {
            arg1.Update(arg2);
            return arg1;
        }

        #endregion
    }
}
