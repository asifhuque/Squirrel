using Newtonsoft.Json;

namespace Squirrel.Domain
{
    [JsonObject("settings")]
    public class Setttings : ResponseObject
    {
        /// <summary>
        /// Gets or sets pings for the settings.
        /// </summary>
        [JsonProperty("pings")]
        public string Pings
        {
            get { return pings; }
            set
            {
                pings = value;
                OnPropertyChanged("pings");
            }
        }

        /// <summary>
        /// Gets or sets a value whether to send the update to twitter.
        /// </summary>
        [JsonProperty("sendtotwitter")]
        public bool SendToTwitter
        {
            get { return sendToTwitter; }
            set
            {
                sendToTwitter = value;
                OnPropertyChanged("sendtotwitter");
            }
        }

        /// <summary>
        /// Gets or sets a value whether to send the update to facebook.
        /// </summary>
        [JsonProperty("sendtofacebook")]
        public bool SendToFacebook
        {
            get { return sendToFacebook; }
            set
            {
                sendToFacebook = value;
                OnPropertyChanged("sendtofacebook");
            }
        }

        private string pings;
        private bool sendToTwitter;
        private bool sendToFacebook;
   
    }
}
