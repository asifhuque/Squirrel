﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Arthus.Domain;

namespace Arthus
{
    public class VenueResponse : ResponseObject
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
