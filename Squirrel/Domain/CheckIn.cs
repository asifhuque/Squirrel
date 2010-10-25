using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Squirrel.Domain
{
    public class CheckIn : ResponseObject
    {
        /// <summary>
        /// Gets or sets pings for the settings.
        /// </summary>
        [JsonProperty("ismayor")]
        public bool IsMayor
        {
            get { return isMayor; }
            set
            {
                isMayor = value;
                OnPropertyChanged("IsMayor");
            }
        }

        /// <summary>
        /// Gets or sets email of the user.
        /// </summary>
        [JsonProperty("display")]
        public string DisplayMessage
        {
            get { return display; }
            set
            {
                display = value;
                OnPropertyChanged("Display");
            }
        }

        /// <summary>
        /// Gets or sets email of the user.
        /// </summary>
        [JsonProperty("venue")]
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                OnPropertyChanged("Venue");
            }
        }

        private Venue venue;
        private bool isMayor;
        private string display;
    }
}
