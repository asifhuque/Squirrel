using Newtonsoft.Json;

namespace SuperNova.Domain
{
    public class Badge : ResponseObject
    {
        /// <summary>
        ///  Get or sets id of the user
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
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("name")]
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
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("icon")]
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("description")]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private string description;
        private int id;
        private string name;
        private string icon;
    }
}
