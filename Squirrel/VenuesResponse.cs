using System.Collections.Generic;
using Newtonsoft.Json;
using Squirrel.Domain;
using Squirrel.Domain.Base;

namespace Squirrel
{
    public class VenuesResponse : ResponseObject
    {
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

        private IList<VenueGroup> groups = new List<VenueGroup>();
    }
}
