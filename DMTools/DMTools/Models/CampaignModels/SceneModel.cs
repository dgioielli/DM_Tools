using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.CampaignModels
{
    public class SceneModel : IObjectBase
    {
        #region Variables and Properties

        public string ID { get; set; }
        public string Name { get; set; }
        public string PlayerGoal { get; set; }
        public string StorytellerGoal { get; set; }
        public string SceneType { get; set; }
        public List<string> Notes { get; protected set; }

        [JsonIgnore]
        public string ShowName => $"{SceneType} :: {Name}";


        #endregion

        #region Constructors

        public SceneModel()
        {
            ID = "";
            Name = "";
            PlayerGoal = "";
            StorytellerGoal = "";
            SceneType = "";
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Scene > {Name} :: {StorytellerGoal}//{PlayerGoal}";

        #endregion
    }
}
