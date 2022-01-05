using DMTools.Keys;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Models.SessionModels;
using DMTools.Repositories;
using DMTools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DMTools.View.ContentViewer
{
    class ContentViewerSectionModel : ContentViewerViewModel
    {
        #region Variables and Properties

        SessionRepository Repository => SessionRepository.GetInstance();
        CharacterRepository CharRepository => CharacterRepository.GetInstance();

        SessionModel m_model;

        public string WDW_Title { get => $"DM Tools - Section : {m_model.SessionName}"; }

        #endregion

        #region Constructors

        public ContentViewerSectionModel(SessionModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.SessionName}");
            AddHeading2(result, $"Introduction:");
            AddText(result, $"{m_model.SessionIntro}");
            AddHeading2(result, $"Possibilities:");
            m_model.Possibilities.ForEach(x => { if (x.WasIgnored) AddStrikeoutText(result, x.Text); else AddText(result, x.Text); });
            AddHeading2(result, $"NPCs:");
            AddHeading3(result, $"Allies:");
            m_model.Characters.FindAll(x => x.Role == ECharacterRoleKeys.Ally).ForEach(x => AddText(result, FlowDocumentService.GetSessionCharacterRuns(x)));
            AddHeading3(result, $"Enemies:");
            m_model.Characters.FindAll(x => x.Role == ECharacterRoleKeys.Enemy).ForEach(x => AddText(result, FlowDocumentService.GetSessionCharacterRuns(x)));
            AddHeading3(result, $"Neutrals:");
            m_model.Characters.FindAll(x => x.Role == ECharacterRoleKeys.Neutral).ForEach(x => AddText(result, FlowDocumentService.GetSessionCharacterRuns(x)));
            AddHeading3(result, $"Secondaries:");
            m_model.Characters.FindAll(x => x.Role == ECharacterRoleKeys.Secondary).ForEach(x => AddText(result, FlowDocumentService.GetSessionCharacterRuns(x)));
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            return result;
        }

        public override void Update()
        {
            var model = Repository.GetSectionById(m_model.ID);
            m_model = model;
            if (model == null) OnPropertyChanged(PropertyEventKeys.Close);
            else OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
