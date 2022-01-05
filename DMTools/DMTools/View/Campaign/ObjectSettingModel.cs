using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.Campaign
{
    public class ObjectSettingModel
    {
        #region Variables and Properties

        public IObjectSetting Object { get; }
        private Action<string> m_update;
        private Action<string> m_delete;
        private Action<string> m_duplicate;
        private Action<string> m_showContent;

        #endregion

        #region Constructors

        public ObjectSettingModel(IObjectSetting obj, Action<string> update, Action<string> delete, Action<string> duplicate, Action<string> showContent)
        {
            Object = obj;
            m_update = update;
            m_delete = delete;
            m_duplicate = duplicate;
            m_showContent = showContent;
        }

        #endregion

        #region Functions

        internal void Update() => m_update(Object.ID);

        internal void Delete() => m_delete(Object.ID);

        internal void Duplicate() => m_duplicate(Object.ID);

        internal void ShowContent() => m_showContent(Object.ID);

        #endregion
    }
}
