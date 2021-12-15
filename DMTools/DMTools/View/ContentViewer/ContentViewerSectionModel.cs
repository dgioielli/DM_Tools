using DMTools.Keys;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace DMTools.View.ContentViewer
{
    class ContentViewerSectionModel : ContentViewerViewModel
    {
        #region Variables and Properties

        SectionRepository Repository => SectionRepository.GetInstance();

        SectionModel m_model;

        public string WDW_Title { get => $"DM Tools - Section : {m_model.SectionName}"; }

        #endregion

        #region Constructors

        public ContentViewerSectionModel(SectionModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.SectionName}");
            AddHeading2(result, $"Introduction:");
            AddText(result, $"{m_model.SectionIntro}");
            AddHeading2(result, $"Possibilities:");
            m_model.Possibilities.ForEach(x => { if (x.WasIgnored) AddStrikeoutText(result, x.Text); else AddText(result, x.Text); });
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
