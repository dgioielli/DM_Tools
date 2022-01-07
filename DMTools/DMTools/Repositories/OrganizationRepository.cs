using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class OrganizationRepository : ObjectBaseRepository<OrganizationModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static OrganizationRepository m_instance = new OrganizationRepository();

        public override List<OrganizationModel> Objects => Repository.Model.Setting.Organizations;

        #endregion

        #region Constructors

        public static OrganizationRepository GetInstance()
        { if (m_instance == null) m_instance = new OrganizationRepository(); return m_instance; }

        private OrganizationRepository()
        { m_update = Repository.Model.Setting.UpdateOrganizations; }

        #endregion

        #region Functions

        protected override void CopyInfo(OrganizationModel model, OrganizationModel result)
        {
            result.Name = model.Name;
            result.Concept = model.Concept;
            result.OrganizationType = model.OrganizationType;
            model.Notes.ForEach(x => result.Notes.Add(x));
        }

        public List<string> GetAllTypes() => GetAllData(x => x.OrganizationType);

        #endregion
    }
}
