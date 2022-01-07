using DMTools.Models.SettingModels;
using DMTools.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Models.SettingModels
{
    public class EventModel : IObjectBase
    {
        #region Variables and Properties

        LocationRepository LocRepository => LocationRepository.GetInstance();

        public string ID { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string EventType { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string LocationID { get; set; }
        public List<CharacterEventModel> Participants { get; set; }
        public List<string> Notes { get; protected set; }

        [JsonIgnore]
        public string ShowName => $"{Year}-{Month}-{Day} :: {Name}";

        #endregion

        #region Constructors

        public EventModel()
        {
            ID = "";
            Name = "";
            Abstract = "";
            EventType = "";
            LocationID = "";
            Participants = new List<CharacterEventModel>();
            Day = 0;
            Month = 0;
            Year = 0;
            Notes = new List<string>();
        }

        #endregion

        #region Functions

        public override string ToString() => $"Event > {Name} :: {Abstract}";

        public void SetLocation(string locationShwoName)
        {
            var loc = LocRepository.GetObjectShowName(locationShwoName);
            if (loc == null) LocationID = "";
            else LocationID = loc.ID;
        }

        public LocationModel GetLocation() => LocRepository.GetObjectById(LocationID);

        #endregion
    }
}
