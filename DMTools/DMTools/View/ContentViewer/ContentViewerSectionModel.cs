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
            AddTitle(result);
            AddSectionIntro(result);
            return result;
        }

        private void AddSectionIntro(FlowDocument result)
        {
            var par = new Paragraph() { FontSize = 12, FontWeight = FontWeights.Bold };
            par.Inlines.Add(new Run($"Introduction:"));
            result.Blocks.Add(par);
            par = new Paragraph() { FontSize = 12 };
            par.Inlines.Add(new Run($"{m_model.SectionIntro}"));
            result.Blocks.Add(par);
        }

        private void AddTitle(FlowDocument result)
        {
            var par = new Paragraph() { FontSize = 36, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            par.Inlines.Add(new Run($"{m_model.SectionName}"));
            result.Blocks.Add(par);
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
