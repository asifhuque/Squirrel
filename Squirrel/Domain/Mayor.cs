using Newtonsoft.Json;

namespace Squirrel.Domain
{
    public class Mayor : ResponseObject
    {
        /// <summary>
        /// Gets or sets the type for the mayor
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
        /// Gets or set the number of checkins for the mayor 
        /// </summary>
        [JsonProperty("checkins")]
        public int CheckIns
        {
            get { return checkIns; }
            set
            {
                checkIns = value;
                OnPropertyChanged("CheckIns");
            }
        }

        /// <summary>
        /// Gets or set the user
        /// </summary>
        [JsonProperty("user")]
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        ///  Gets or sets the message from the mayor.
        /// </summary>
        [JsonProperty("message")]
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private User user;
        private string type;
        private int checkIns;
        private string message;
    }
}
