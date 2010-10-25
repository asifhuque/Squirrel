using Newtonsoft.Json;
using System.Collections.Generic;

namespace Squirrel.Domain
{
    public class User : ResponseObject
    {
        /// <summary>
        /// Gets or sets firstName of the user
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
        /// Gets or sets lastName of the user.
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
        /// Gets or sets friend status of the user.
        /// </summary>
        [JsonProperty("friendstatus")]
        public string FriendStatus
        {
            get { return friendStatus; }
            set
            {
                friendStatus = value;
                OnPropertyChanged("FriendStatus");
            }
        }

        /// <summary>
        /// Gets or sets home city of the user.
        /// </summary>
        [JsonProperty("homecity")]
        public string HomeCity
        {
            get { return homecity; }
            set
            {
                homecity = value;
                OnPropertyChanged("HomeCity");
            }
        }

        /// <summary>
        /// Gets or sets photo of the user.
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
        /// Gets or sets gender of the user.
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
        /// Gets or sets email of the user.
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
        /// Gets or sets settings of the user.
        /// </summary>
        [JsonProperty("settings")]
        public Setttings Setttings
        {
            get { return settings; }
            set
            {
                settings = value;
                OnPropertyChanged("Settings");
            }
        }

        /// <summary>
        /// Gets or sets status of the user.
        /// </summary>
        [JsonProperty("status")]
        public Status Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets status of the user.
        /// </summary>
        [JsonProperty("checkin")]
        public CheckIn CheckIn
        {
            get { return checkIn; }
            set
            {
                checkIn = value;
                OnPropertyChanged("CheckIn");
            }
        }

        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("badges")]
        public IList<Badge> Badges
        {
            get { return badges; }
            set
            {
                badges = value;
                OnPropertyChanged("Badges");
            }
        }

        private string firstName;
        private string lastName;
        private string friendStatus;
        private string homecity;
        private string photoUrl;
        private string gender;
        private string email;
        private Setttings settings;
        private Status status;
        private CheckIn checkIn;

        private IList<Badge> badges = new List<Badge>();

    }
}
