using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Squirrel.Domain;

namespace Squirrel
{
    public class VenueResponse : Domain.Base.ResponseObject
    {
        /// <summary>
        /// Gets or sets the venue object.
        /// </summary>
        [JsonProperty("venue")]
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                OnPropertyChanged("Groups");
            }
        }

        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("groups")]
        public IList<VenueGroup> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }

        private Venue venue;
        private IList<VenueGroup> groups = new List<VenueGroup>();
    }
}
