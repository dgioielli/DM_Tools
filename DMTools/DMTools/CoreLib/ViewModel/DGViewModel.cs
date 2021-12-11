using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.ViewModel
{
    public class DGViewModel : INotifyPropertyChanged
    {
        #region Construtoras

        protected DGViewModel()
        { assinarComandos(); }

        #endregion Construtoras

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null && propName != "")
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion INotifyPropertyChanged

        #region Funções

        protected virtual void assinarComandos()
        { }

        #endregion Funções
    }

}
