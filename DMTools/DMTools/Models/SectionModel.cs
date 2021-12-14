using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models
{
    public class SectionModel
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string SectionName { get; set; }
        public string SectionIntro { get; set; }
        public List<object> Possibilities { get; protected set; }
        public List<object> Characters { get; protected set; }
        public List<object> Locations { get; protected set; }
        public List<object> Organizations { get; protected set; }
        public List<object> Encounters { get; protected set; }
        public List<string> Notes { get; protected set; }


        #endregion

        #region Constructors

        public SectionModel()
        {
            ID = "";
            SectionName = "New Section";
            SectionIntro = "";
            Possibilities = new List<object>();
            Characters = new List<object>();
            Locations = new List<object>();
            Organizations = new List<object>();
            Encounters = new List<object>();
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Section > {SectionName}";

        #endregion
    }
}
