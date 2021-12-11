using DMTools.Managers.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Managers
{
    public class ObserverManager
    {
        #region Variables and properties

        private static ObserverManager m_instance = new ObserverManager();

        //private List<ISectionObserver> m_sections = new List<ISectionObserver>();
        private List<IGeneralObserver> m_observers = new List<IGeneralObserver>();
        private List<IGeneralObserver> m_queueClear = new List<IGeneralObserver>();

        #endregion

        #region Constructors

        public static ObserverManager GetInstance()
        { if (m_instance == null) m_instance = new ObserverManager(); return m_instance; }

        private ObserverManager()
        { }

        #endregion

        #region Functions

        //public void RegisterSectionObserver(ISectionObserver observer)
        //{ if (!m_sections.Contains(observer)) m_sections.Add(observer); }

        //public void UnregisterSectionObserver(ISectionObserver observer)
        //{ while (m_sections.Contains(observer)) m_sections.Remove(observer); }

        //public void UpdateSectionObserver() => m_sections.ForEach(x => x.UpdateSection());

        public void RegisterGeneralObserver(IGeneralObserver observer)
        { if (!m_observers.Contains(observer)) m_observers.Add(observer); }

        private void UnregisterGeneralObserver(IGeneralObserver observer)
        { while (m_observers.Contains(observer)) m_observers.Remove(observer); }

        public void QueueUnregisterGeneralObserver(IGeneralObserver observer)
        { if (!m_queueClear.Contains(observer)) m_queueClear.Add(observer); }

        public void UpdateGeneralObserver()
        { m_observers.ForEach(x => x.Update()); m_queueClear.ForEach(x => UnregisterGeneralObserver(x)); }

        #endregion
    }
}
