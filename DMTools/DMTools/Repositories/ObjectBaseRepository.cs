using DMTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    public abstract class ObjectBaseRepository<T> : GenericRepository<T>
        where T : IObjectBase, new()
    {
        #region Functions

        protected override bool CheckObject(T obj) => GetObject(obj) != null;

        public override T GetNewObject() => new T() { ID = GetNewID() };

        protected override T GetObject(T model) => GetObjectById(model.ID);

        public override T GetObjectById(string id) => Objects.Find(x => x.ID == id);

        public override T GetCopy(T model)
        {
            var result = new T() { ID = model.ID };
            CopyInfo(model, result);
            return result;
        }

        public List<IObjectBase> GetAllObjectsBase()
        {
            var result = new List<IObjectBase>();
            Objects.ForEach(x => result.Add(x));
            return result;
        }

        public T GetObjectShowName(string shwoName) => Objects.Find(x => x.ShowName == shwoName);


        #endregion
    }
}
