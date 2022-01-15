using DMTools.CoreLib.PoolItems;
using DMTools.Models;
using DMTools.View.Components.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DMTools.View.EditorView.Components.NotesObject
{
    class NotesListManager<T>
        where T : IObjectBase
    {
        #region Variables and Properties

        PoolGeneric<EditableTextBlock, string> m_poolNotes;
        BaseObjectEditorViewModel<T> m_vm;

        List<EditableTextBlock> m_notes = new List<EditableTextBlock>();

        #endregion

        #region Constructors

        public NotesListManager(BaseObjectEditorViewModel<T> vm)
        {
            m_vm = vm;
            m_poolNotes = new PoolGeneric<EditableTextBlock, string>(NewNote, RefreshNote);
        }

        #endregion

        #region Functions

        public void ShowNotes(StackPanel pnl)
        {
            var list = new List<string>(m_vm.LST_Notes);
            list.Add("");
            m_notes = m_poolNotes.GetObjects(list);
            pnl.Children.Clear();
            m_notes.ForEach(x => pnl.Children.Add(x));
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

        #endregion
    }
}
