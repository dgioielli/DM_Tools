using DMTools.Keys;
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
    class ContentViewerOrganizationModel : ContentViewerViewModel
    {
        #region Variables and Properties

        OrganizationRepository Repository => OrganizationRepository.GetInstance();
        SessionRepository SessionRepository => SessionRepository.GetInstance();

        OrganizationModel m_model;

        public string WDW_Title { get => $"DM Tools - Location : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerOrganizationModel(OrganizationModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            AddHeading2(result, $"{m_model.Concept}");
            if (m_model.OrganizationType != "") AddHeading2(result, $"{m_model.OrganizationType}");
            AddHeading3(result, $"Members:");
            m_model.Members.ForEach(x => AddText(result, FlowDocumentService.GetCharacterInfoRuns(x)));
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
