using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class CharacterRepository
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static CharacterRepository m_instance = new CharacterRepository();

        public List<CharacterModel> Characters { get => Repository.Model.Setting.Characters; }

        #endregion

        #region Constructors

        public static CharacterRepository GetInstance()
        { if (m_instance == null) m_instance = new CharacterRepository(); return m_instance; }

        private CharacterRepository()
        { }

        #endregion

        #region Functions

        public CharacterModel GetNewCharacter()
        {
            var result = new CharacterModel() { ID = GetNewID() };
            return result;
        }

        private string GetNewID()
        {
            var now = DateTime.Now;
            return $"Character:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        internal void AddEditCharacter(CharacterModel model)
        {
            if (!Characters.Exists(x => x.ID == model.ID)) AddCharacter(model);
            else EditCharacter(model, Characters.Find(x => x.ID == model.ID));
        }

        internal CharacterModel GetCharacterById(string id) => Characters.Find(x => x.ID == id);

        private void EditCharacter(CharacterModel model, CharacterModel oldModel)
        {
            Characters.Remove(oldModel);
            AddCharacter(model);
        }

        private void AddCharacter(CharacterModel model)
        {
            Characters.Add(model);
            Repository.Model.Update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteCharacter(CharacterModel model)
        {
            Characters.Remove(model);
            Observer.UpdateGeneralObserver();
        }

        internal CharacterModel GetCopy(CharacterModel model)
        {
            var result = new CharacterModel() { ID = model.ID };
            CopyInfo(model, result);
            return result;
        }

        internal CharacterModel GetDuplicate(CharacterModel model)
        {
            var result = new CharacterModel() { ID = GetNewID() };
            CopyInfo(model, result);
            return result;
        }

        private static void CopyInfo(CharacterModel model, CharacterModel result)
        {
            result.Name = model.Name;
            result.Concept = model.Concept;
            result.Class = model.Class;
            result.Race = model.Race;
            result.Clan = model.Clan;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        internal List<string> GetAllClass() => GetAllData(x => x.Class);

        internal List<string> GetAllRaces() => GetAllData(x => x.Race);

        internal List<string> GetAllClans() => GetAllData(x => x.Clan);

        protected List<string> GetAllData(Func<CharacterModel, string> getData)
        {
            var result = new List<string>();
            Characters.ForEach(x => result.Add(getData(x)));
            return result.Distinct().OrderBy(x => x).ToList();
        }

        #endregion
    }
}
