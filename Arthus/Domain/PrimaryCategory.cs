using Newtonsoft.Json;

namespace Arthus.Domain
{
    public class PrimaryCategory : ResponseObject
    {
        /// <summary>
        ///  Get or sets id of the category
        /// </summary>
        [JsonProperty("id")]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///  Get or sets id of the venue
        /// </summary>
        [JsonProperty("fullpathname")]
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        /// <summary>
        ///  Get or sets id of the venue
        /// </summary>
        [JsonProperty("nodename")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///  Get or sets id of the venue
        /// </summary>
        [JsonProperty("iconurl")]
        public string IconUrl
        {
            get { return iconUrl; }
            set
            {
                iconUrl = value;
                OnPropertyChanged("IconUrl");
            }
        }

        private int id;
        private string fullName;
        private string name;
        private string iconUrl;
    }
}
