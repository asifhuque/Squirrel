using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Arthus.Domain
{
    [JsonObject("status")]
    public class Status : ResponseObject
    {
        /// <summary>
        /// Gets or sets settings of the user.
        /// </summary>
        [JsonProperty("friendrequests")]
        public int FriendRequestsCount
        {
            get { return friendRequests; }
            set
            {
                friendRequests = value;
                OnPropertyChanged("FriendRequestsCount");
            }
        }

        private int friendRequests;
    }
}
