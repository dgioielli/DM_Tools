using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.PoolItems
{
    class PoolGeneric<T, U>
    {
        #region Variables and Properties

        private List<T> m_pool = new List<T>();

        private Func<U, T> m_newObject;
        private Func<T, U, T> m_refreshObject;

        #endregion

        #region Constructors

        public PoolGeneric(Func<U, T> newObject, Func<T, U, T> refreshObject)
        {
            m_newObject = newObject;
            m_refreshObject = refreshObject;
        }

        #endregion

        #region Functions

        public List<T> GetObjects(List<U> list)
        {
            var result = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i < m_pool.Count) result.Add(m_refreshObject(m_pool[i], list[i]));
                else result.Add(m_newObject(list[i]));
            }
            return result;
        }

        #endregion
    }
}
