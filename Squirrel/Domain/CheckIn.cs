using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Checkin : ResponseObject
    {
        /// <summary>
        /// Gets the type of the special.
        /// </summary>
        [JsonProperty("type")]
        public CheckinType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        /// <summary>
        /// Gets or sets email of the user.
        /// </summary>
        [JsonProperty("private")]
        public bool IsPrivate
        {
            get { return isPrivate; }
            set
            {
                isPrivate = value;
                OnPropertyChanged("IsPrivate");
            }
        }


        /// <summary>
        /// Gets or sets timezone for the checkin.
        /// </summary>
        [JsonProperty("timeZone")]
        public string TimeZone
        {
            get { return timezone; }
            set
            {
                timezone = value;
                OnPropertyChanged("TimeZone");
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

        /// <summary>
        /// Gets or sets the location for the check-in
        /// </summary>
        [JsonProperty("location")]
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        private CheckinType type;
        private Venue venue;
        private bool isPrivate;
        private Location location;
        private string timezone;

    }
}
