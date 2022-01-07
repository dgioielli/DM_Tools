using DMTools.Managers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class CharacterRepository : ObjectBaseRepository<CharacterModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static CharacterRepository m_instance = new CharacterRepository();

        public override List<CharacterModel> Objects => Repository.Model.Setting.Characters;

        #endregion

        #region Constructors

        public static CharacterRepository GetInstance()
        { if (m_instance == null) m_instance = new CharacterRepository(); return m_instance; }

        private CharacterRepository()
        { m_update = Repository.Model.Setting.UpdateCharacters; }

        #endregion

        #region Functions

        protected override void CopyInfo(CharacterModel model, CharacterModel result)
        {
            result.Name = model.Name;
            result.Concept = model.Concept;
            result.Class = model.Class;
            result.Race = model.Race;
            result.Clan = model.Clan;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        public List<string> GetAllClass() => GetAllData(x => x.Class);

        public List<string> GetAllRaces() => GetAllData(x => x.Race);

        public List<string> GetAllClans() => GetAllData(x => x.Clan);

        #endregion
    }
}
