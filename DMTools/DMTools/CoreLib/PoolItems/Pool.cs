using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.PoolItems
{
    public class Pool <T, U>
        where T : IReusablePool<U>, new()
    {
        #region Variables and Properties

        private List<T> m_pool = new List<T>();

        #endregion

        #region Constructors

        public Pool()
        { }

        #endregion

        #region Functions

        public List<T> GetObjects(List<U> list)
        {
            var result = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i < m_pool.Count)
                {
                    m_pool[i].Update(list[i]);
                    result.Add(m_pool[i]);
                }
                else
                {
                    var newObj = new T();
                    newObj.Update(list[i]);
                    result.Add(newObj);
                    m_pool.Add(newObj);
                }
            }
            return result;
        }

        #endregion
    }
}
