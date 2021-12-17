using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SessionModels
{
    public class PossibilityModel
    {
        #region Variables and Properties

        public string Text { get; set; }
        public bool WasIgnored { get; set; }

        #endregion

        #region Constructors

        public PossibilityModel(PossibilityModel possibility) : this(possibility.Text, possibility.WasIgnored)
        { }

        public PossibilityModel() : this("", false)
        { }

        public PossibilityModel(string text, bool wasIgnored)
        {
            Text = text;
            WasIgnored = wasIgnored;
        }

        #endregion

        #region Functions

        public override string ToString() => $"Possibility :: {Text}";

        #endregion
    }
}
