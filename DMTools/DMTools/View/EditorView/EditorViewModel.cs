using DMTools.CoreLib.ViewModel;
using DMTools.Keys;
using DMTools.Managers;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.View.EditorView
{
    public abstract class EditorViewModel : DGViewModel
    {
        #region Variables and Properties

        protected ObserverManager Observer => ObserverManager.GetInstance();

        public abstract string TXT_Name { get; set; }
        protected abstract string TypeEditor { get; }
        public abstract List<string> LST_Notes { get; }

        public string WDW_Title => $"DM Tools - {TypeEditor} : {TXT_Name}";

        public ICommand BTN_Update { get; protected set; }
        public ICommand BTN_Conclude { get; protected set; }
        public ICommand BTN_Cancel { get; protected set; }

        #endregion

        #region Constructors

        public EditorViewModel()
        { }

        #endregion

        #region Functions

        public abstract void Update();

        protected abstract void UpdateObject();

        protected override void assinarComandos()
        {
            BTN_Update = new DGCommand(obj => UpdateObject());
            BTN_Conclude = new DGCommand(obj => { UpdateObject(); OnPropertyChanged(PropertyEventKeys.Close); });
            BTN_Cancel = new DGCommand(obj => OnPropertyChanged(PropertyEventKeys.Close));
            base.assinarComandos();
        }

        #endregion
    }
}
