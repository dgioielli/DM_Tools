
using DMTools.Models.SessionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models
{
    public class SessionModel
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string SessionName { get; set; }
        public string SessionIntro { get; set; }
        public List<PossibilityModel> Possibilities { get; protected set; }
        public List<SessionCharacterModel> Characters { get; protected set; }
        public List<object> Locations { get; protected set; }
        public List<object> Organizations { get; protected set; }
        public List<object> Encounters { get; protected set; }
        public List<string> Notes { get; protected set; }


        #endregion

        #region Constructors

        public SessionModel()
        {
            ID = "";
            SessionName = "New Section";
            SessionIntro = "";
            Possibilities = new List<PossibilityModel>();
            Characters = new List<SessionCharacterModel>();
            Locations = new List<object>();
            Organizations = new List<object>();
            Encounters = new List<object>();
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Section > {SessionName}";

        #endregion
    }
}
