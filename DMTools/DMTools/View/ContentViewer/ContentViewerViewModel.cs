using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Repositories;
using DMTools.View.SectionEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace DMTools.View.ContentViewer
{
    public abstract class ContentViewerViewModel : DGViewModel, IGeneralObserver
    {
        #region Variables and Properties

        #endregion

        #region Constructors

        public ContentViewerViewModel()
        { }

        #endregion

        #region Functions

        protected override void assinarComandos()
        {
            base.assinarComandos();
        }

        public abstract FlowDocument GetDocument();

        public abstract void Update();

        #region Insertion Block


        protected void AddHeading1(FlowDocument result, string text) => AddHeading1(result, new Run(text));

        protected void AddHeading1(FlowDocument result, params Inline[] inlines) => AddParagraph(result, inlines, 36, FontWeights.Bold, TextAlignment.Center);

        protected void AddHeading2(FlowDocument result, string text) => AddHeading2(result, new Run(text));

        protected void AddHeading2(FlowDocument result, params Inline[] inlines) => AddParagraph(result, inlines, 20, FontWeights.Bold);

        protected void AddText(FlowDocument result, string text) => AddText(result, new Run(text));

        protected void AddText(FlowDocument result, params Inline[] inlines) => AddParagraph(result, inlines);

        protected void AddStrikeoutText(FlowDocument result, string text) => AddStrikeoutText(result, new Run(text));

        protected void AddStrikeoutText(FlowDocument result, params Inline[] inlines) => AddParagraph(result, inlines, decoration: TextDecorations.Strikethrough);

        protected void AddParagraph(FlowDocument result, Inline[] inlines, double fontSize = 12, FontWeight fontWeight = default(FontWeight), TextAlignment textAlignment = TextAlignment.Left, FontStyle style = default(FontStyle), TextDecorationCollection decoration = default(TextDecorationCollection)) => result.Blocks.Add(GetParagraph(inlines, fontSize, fontWeight, textAlignment));

        private Paragraph GetParagraph(string text) => GetParagraph(new Inline[] { new Run(text) });

        private Paragraph GetParagraph(Inline[] inlines, double fontSize = 12, FontWeight fontWeight = default(FontWeight), TextAlignment textAlignment = TextAlignment.Left, FontStyle style = default(FontStyle), TextDecorationCollection decoration = default(TextDecorationCollection))
        {
            var par = new Paragraph() { FontSize = fontSize, FontWeight = fontWeight, TextAlignment = textAlignment, FontStyle = style, TextDecorations = decoration };
            foreach (var run in inlines)
                par.Inlines.Add(run);
            return par;
        }

        protected void AddList(FlowDocument result, List<string> list)
        {
            var listResult = new List();
            foreach (var line in list)
                listResult.ListItems.Add(new ListItem(GetParagraph(line)));
            result.Blocks.Add(listResult);
        }

        #endregion

        #endregion
    }
}
