using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models
{
    public class Section
    {
        #region Variables and Properties

        public string SectionName { get; set; }
        public List<object> Possibilities { get; protected set; }
        public List<object> Characters { get; protected set; }
        public List<object> Locations { get; protected set; }
        public List<object> Organizations { get; protected set; }


        #endregion

        #region Constructors

        #endregion

        #region Functions

        #endregion
    }
}
