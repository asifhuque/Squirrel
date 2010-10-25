using Newtonsoft.Json;

namespace Squirrel.Domain
{
    public class Stats : ResponseObject
    {
        /// <summary>
        /// Gets or sets pings for the settings.
        /// </summary>
        [JsonProperty("herenow")]
        public int HereNow
        {
            get { return hereNow; }
            set
            {
                hereNow = value;
                OnPropertyChanged("HereNow");
            }
        }

        private int hereNow;
    }
}
