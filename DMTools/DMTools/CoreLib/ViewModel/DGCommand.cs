using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMTools.CoreLib.ViewModel
{
    public class DGCommand : ICommand
    {
        #region Variáveis e Propriedades

        private Action<object> _execteMethod;
        private Func<object, bool> _canexecuteMethod;

        #endregion Variáveis e Propriedades

        #region Construtoras

        public DGCommand(Action<object> execteMethod) : this(execteMethod, (obj) => { return true; })
        { }

        public DGCommand(Action<object> execteMethod, Func<object, bool> canexecuteMethod)
        {
            _execteMethod = execteMethod;
            _canexecuteMethod = canexecuteMethod;
        }

        #endregion Construtoras

        public bool CanExecute(object parameter)
        {
            if (_canexecuteMethod != null)
            {
                return _canexecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execteMethod(parameter);
        }
    }

}
