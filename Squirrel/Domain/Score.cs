using Newtonsoft.Json;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Score : ResponseObject
    {
        /// <summary>
        ///  Gets or sets the points for a checkin.
        /// </summary>
        [JsonProperty("points")]
        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged("Points");
            }
        }

        /// <summary>
        ///  Gets or sets the points for a checkin.
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
        ///  Gets or sets the points for a checkin.
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

        private string message;
        private int points;
        private string icon;
    }
}
