using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.Logger
{
    public abstract class BaseLog : ILog
    {
        #region Variáveis e propriedades

        protected string m_msg = "";
        protected string m_tracker = "";
        protected DateTime m_instante = DateTime.Now;
        protected string m_user = Environment.UserName;

        #endregion

        #region Construtoras e instanciadores

        protected BaseLog(string tracker, string msg)
        {
            m_tracker = tracker;
            m_msg = msg;
        }

        #endregion

        #region Funções

        public abstract string getStringLog();

        #endregion
    }


}
