using DMTools.Models.CampaignModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    public class AdventureRepository : ObjectBaseRepository<AdventureModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static AdventureRepository m_instance = new AdventureRepository();

        public override List<AdventureModel> Objects => Repository.Model.Adventures;

        #endregion

        #region Constructors

        public static AdventureRepository GetInstance()
        { if (m_instance == null) m_instance = new AdventureRepository(); return m_instance; }

        private AdventureRepository()
        { m_update = Repository.Model.UpdateAdventures; }

        #endregion

        #region Functions

        protected override void CopyInfo(AdventureModel model, AdventureModel result)
        {
            result.Abstract = model.Abstract;
            result.Name = model.Name;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        public List<string> GetAllTypes() => GetAllData(x => x.AdventureType);

        #endregion
    }
}
