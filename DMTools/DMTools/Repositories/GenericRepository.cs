using DMTools.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    public abstract class GenericRepository<T>
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        public abstract List<T> Objects { get; }
        protected Action m_update;

        #endregion

        #region Constructors

        #endregion

        #region Functions

        public abstract T GetNewObject();

        protected abstract bool CheckObject(T obj);

        protected abstract T GetObject(T model);

        public abstract T GetObjectById(string id);

        public abstract T GetCopy(T model);

        protected abstract void CopyInfo(T model, T result);

        protected string GetNewID()
        {
            var now = DateTime.Now;
            return $"{typeof(T)}:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        public virtual void AddEditObject(T model)
        {
            if (!Objects.Exists(CheckObject)) AddObject(model);
            else EditObject(model, GetObject(model));
        }

        private void EditObject(T model, T oldModel)
        {
            Objects.Remove(oldModel);
            AddObject(model);
        }

        protected virtual void AddObject(T model)
        {
            Objects.Add(model);
            m_update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteObject(T model)
        {
            Objects.Remove(GetObject(model));
            Observer.UpdateGeneralObserver();
        }

        public virtual T GetDuplicate(T model)
        {
            var result = GetNewObject();
            CopyInfo(model, result);
            return result;
        }

        protected List<string> GetAllData(Func<T, string> getData)
        {
            var result = new List<string>();
            Objects.ForEach(x => result.Add(getData(x)));
            return result.Distinct().OrderBy(x => x).ToList();
        }

        #endregion
    }
}
