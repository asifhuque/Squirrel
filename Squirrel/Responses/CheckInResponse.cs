using Newtonsoft.Json;
using System;
using Squirrel.Domain;
using System.Collections.Generic;
using Squirrel.Domain.Base;

namespace Squirrel
{
    public class CheckinResponse : ResponseObject
    {
        /// <summary>
        /// Gets or sets status of the user.
        /// </summary>
        [JsonProperty("checkin")]
        public Checkin CheckIn
        {
            get { return checkin; }
            set
            {
                checkin = value;
                OnPropertyChanged("CheckIn");
            }
        }

        private Checkin checkin;
    }

}
