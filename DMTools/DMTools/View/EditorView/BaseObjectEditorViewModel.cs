using DMTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView
{
    public abstract class BaseObjectEditorViewModel<T> : EditorViewModel
        where T : IObjectBase
    {
        #region Variables and Properties

        protected T m_model;

        public override string TXT_Name { get => m_model.Name; set { m_model.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(WDW_Title)); } }
        public override List<string> LST_Notes { get => m_model.Notes; }

        #endregion

        #region Constructors

        public BaseObjectEditorViewModel(T model)
        {
            m_model = model;
        }

        #endregion

        #region Functions

        public override void Update()
        {
            OnPropertyChanged(nameof(TXT_Name));
            OnPropertyChanged(nameof(LST_Notes));
        }

        public void SetNotes(List<string> notes)
        {
            m_model.Notes.Clear();
            foreach (var note in notes)
                if (note != "") m_model.Notes.Add(note);
            OnPropertyChanged(nameof(LST_Notes));
        }

        protected void UpdateList<T>(List<T> originalList, List<T> currentList, Func<T, bool> checkFunc)
        {
            originalList.Clear();
            ClearList(currentList, checkFunc);
            originalList.AddRange(currentList);
        }

        protected void ClearList<T>(List<T> list, Func<T, bool> checkFunc)
        {
            var empties = list.FindAll(x => checkFunc(x));
            empties.ForEach(x => list.Remove(x));
        }

        #endregion
    }
}
