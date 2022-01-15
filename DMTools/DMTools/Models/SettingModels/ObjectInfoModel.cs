using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class ObjectInfoModel
    {
        #region Variables and Properties

        public string ObjectId { get; set; }
        public string Info { get; set; }

        #endregion

        #region Constructors

        public ObjectInfoModel() : this("", "")
        { }

        public ObjectInfoModel(string characterId, string info)
        {
            ObjectId = characterId;
            Info = info;
        }

        public ObjectInfoModel(ObjectInfoModel x) : this(x.ObjectId, x.Info)
        { }

        #endregion

        #region Functions

        #endregion
    }
}
