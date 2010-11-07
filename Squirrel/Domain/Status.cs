using Newtonsoft.Json;
using Squirrel.Domain.Base;


namespace Squirrel.Domain
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
