using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.CampaignModels
{
    public class PlotModel : IObjectBase
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string PlotType { get; set; }
        public List<string> Notes { get; protected set; }
        public List<string> IdScenes { get; protected set; }

        [JsonIgnore]
        public string ShowName => $"{PlotType} :: {Name}";


        #endregion

        #region Constructors

        public PlotModel()
        {
            ID = "";
            Name = "";
            Abstract = "";
            PlotType = "";
            Notes = new List<string>();
            IdScenes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Plot > {Name} :: {PlotType}//{Abstract}";

        #endregion
    }
}
