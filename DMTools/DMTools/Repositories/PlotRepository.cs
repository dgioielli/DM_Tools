using DMTools.Models.CampaignModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Repositories
{
    public class PlotRepository : ObjectBaseRepository<PlotModel>
    {
        #region Variables and Properties

        CampaignRepository Repository => CampaignRepository.GetInstance();

        private static PlotRepository m_instance = new PlotRepository();

        public override List<PlotModel> Objects => Repository.Model.Plots;

        #endregion

        #region Constructors

        public static PlotRepository GetInstance()
        { if (m_instance == null) m_instance = new PlotRepository(); return m_instance; }

        private PlotRepository()
        { m_update = Repository.Model.UpdatePlots; }

        #endregion

        #region Functions

        protected override void CopyInfo(PlotModel model, PlotModel result)
        {
            result.Abstract = model.Abstract;
            result.Name = model.Name;
            model.Notes.ForEach(x => result.Notes.Add(x));
            result.PlotType = model.PlotType;
        }

        public List<string> GetAllTypes() => GetAllData(x => x.PlotType);

        #endregion
    }
}
