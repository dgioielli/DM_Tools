using DMTools.CoreLib.PoolItems;
using DMTools.CoreLib.ViewModel;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.View.EditorView.Components
{
    class CharacterInfoControlModel : DGViewModel
    {
        #region Variables and Properties

        CharacterRepository CharRepository => CharacterRepository.GetInstance();

        ObjectInfoModel m_model;

        public string TXT_Info { get => m_model.Info; set { m_model.Info = value; OnPropertyChanged(); } }
        public CharacterModel Character { get => CharRepository.GetObjectById(m_model.ObjectId); set => m_model.ObjectId = value.ID; }

        #endregion

        #region Constructors

        public CharacterInfoControlModel(ObjectInfoModel model)
        {
            Update(model);
        }

        #endregion

        #region Functions

        internal void Update(ObjectInfoModel obj)
        {
            m_model = obj;
            OnPropertyChanged(nameof(TXT_Info));
            OnPropertyChanged(nameof(Character));
        }

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        #endregion
    }
}
