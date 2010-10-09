using System.Collections.Generic;
using Newtonsoft.Json;

namespace SuperNova.Domain
{
    public class VenueGroup : ResponseObject
    {
        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("venues")]
        public IList<Venue> Venues
        {
            get { return venues; }
            set
            {
                venues = value;
                OnPropertyChanged("Venues");
            }
        }

        private IList<Venue> venues = new List<Venue>();
        private string type;
    }
}
