using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.Logger
{
    public class ErrorLog : BaseLog
    {
        #region Variáveis e propriedades

        #endregion

        #region Construtoras e instanciadores

        private ErrorLog(string tracker, string msg) : base(tracker, msg)
        {
            m_tracker = tracker;
            m_msg = msg;
        }

        public static void newLogError(string msg)
        {
            var logger = Logger.getInstance();
            logger.addLog(new ErrorLog(logger.getTracker(), msg));
        }

        #endregion

        #region Funções

        public override string getStringLog() => $"{m_user} :: {m_instante.Year:0000}/{m_instante.Month:00}/{m_instante.Day:00} - {m_instante.Hour:00}:{m_instante.Minute:00} :: (ERROR) {m_tracker} : {m_msg}";

        #endregion
    }
}
