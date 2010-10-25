using Newtonsoft.Json;

namespace Squirrel.Domain
{
    /// <summary>
    /// Contains the friend response.
    /// </summary>
    public class Friend : ResponseObject
    {
        /// <summary>
        /// Gets or sets firstName of the friend
        /// </summary>
        [JsonProperty("firstname")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets lastName of the friend.
        /// </summary>
        [JsonProperty("lastname")]
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets photo of the friend.
        /// </summary>
        [JsonProperty("photo")]
        public string PhotoUrl
        {
            get { return photoUrl; }
            set
            {
                photoUrl = value;
                OnPropertyChanged("Photo");
            }
        }

        /// <summary>
        /// Gets or sets gender of the friend.
        /// </summary>
        [JsonProperty("gender")]
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        /// <summary>
        /// Gets or sets email of the friend.
        /// </summary>
        [JsonProperty("email")]
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Gets or sets email of the friend.
        /// </summary>
        [JsonProperty("twitter")]
        public string Twitter
        {
            get { return twitter; }
            set
            {
                twitter = value;
                OnPropertyChanged("Twitter");
            }
        }

        /// <summary>
        /// Gets or sets email of the friend.
        /// </summary>
        [JsonProperty("facebook")]
        public int Facebook
        {
            get { return facebook; }
            set
            {
                facebook = value;
                OnPropertyChanged("Facebook");
            }
        }


        private string gender;
        private string photoUrl;
        private string email;
        private string twitter;
        private int facebook;
        private string lastName;
        private string firstName;
    }
}
