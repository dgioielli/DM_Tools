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
            return $"Section:{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}_{now.Millisecond}";
        }

        internal void AddEditSection(CharacterModel model)
        {
            if (!Characters.Exists(x => x.ID == model.ID)) AddSection(model);
            else EditSection(model, Characters.Find(x => x.ID == model.ID));
        }

        internal CharacterModel GetSectionById(string id) => Characters.Find(x => x.ID == id);

        private void EditSection(CharacterModel model, CharacterModel oldModel)
        {
            Characters.Remove(oldModel);
            AddSection(model);
        }

        private void AddSection(CharacterModel model)
        {
            Characters.Add(model);
            Repository.Model.Update();
            Observer.UpdateGeneralObserver();
        }

        internal void DeleteSection(CharacterModel model)
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
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        internal List<string> GetAllClass()
        {
            var result = new List<string>();
            Characters.ForEach(x => result.Add(x.Class));
            return result.Distinct().OrderBy(x => x).ToList();
        }

        internal List<string> GetAllRaces()
        {
            var result = new List<string>();
            Characters.ForEach(x => result.Add(x.Race));
            return result.Distinct().OrderBy(x => x).ToList();
        }

        #endregion
    }
}
