using DMTools.Keys;
using DMTools.Models.CampaignModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DMTools.View.ContentViewer
{
    class ContentViewerAdventureModel : ContentViewerViewModel
    {
        #region Variables and Properties

        AdventureRepository Repository => AdventureRepository.GetInstance();
        SessionRepository SessionRepository => SessionRepository.GetInstance();

        AdventureModel m_model;

        public string WDW_Title { get => $"DM Tools - Adventure : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerAdventureModel(AdventureModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            if (m_model.AdventureType != "") AddHeading2(result, $"{m_model.AdventureType}");
            AddText(result, $"{m_model.Abstract}");
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            return result;
        }

        public override void Update()
        {
            var model = Repository.GetObjectById(m_model.ID);
            m_model = model;
            if (model == null) OnPropertyChanged(PropertyEventKeys.Close);
            else OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
