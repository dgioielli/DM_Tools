using DMTools.Keys;
using DMTools.Models;
using DMTools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    class CampaignRepository
    {
        #region Variables and Properties

        private static CampaignRepository m_instance = new CampaignRepository();

        public CampaignModel Model { get; private set; }

        #endregion

        #region Constructors

        public static CampaignRepository GetInstance()
        { if (m_instance == null) m_instance = new CampaignRepository(); return m_instance; }

        private CampaignRepository()
        {
            LoadCurrent();
        }

        #endregion

        #region Functions

        private void LoadCurrent() => LoadData(FilePathKeys.SystemLayersFile);

        internal void LoadData(string filePath)
        {
            Model = new CampaignModel();
            string contentFile;
            FileService.LoadFile(filePath, out contentFile);
            if (contentFile == "") return;
            var result = JsonService.toT<CampaignModel>(contentFile);
            if (result != null) Model = result;
        }

        internal void SaveCurrent() => SaveData(FilePathKeys.SystemLayersFile);

        internal void SaveData(string filePath) => FileService.SaveFile(filePath, JsonService.toJson(Model));

        internal void SetNewCampaign()
        {
            Model = new CampaignModel();
            Model.CampaignName = "New Campaign";
        }

        #endregion
    }
}
