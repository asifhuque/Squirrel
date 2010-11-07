using Newtonsoft.Json;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    /// <summary>
    /// Defines the special tag.
    /// </summary>
    public class Special : ResponseObject
    {
        /// <summary>
        /// Gets the type of the special.
        /// </summary>
        [JsonProperty("type")]
        public SpecialType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        /// <summary>
        /// Gets the kind of  special.
        /// </summary>
        [JsonProperty("kind")]
        public string Kind
        {
            get { return kind; }
            set
            {
                kind = value;
                OnPropertyChanged("Kind");
            }
        }

        /// <summary>
        /// Gets the special message.
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

        /// <summary>
        /// Gets the venue specified.
        /// </summary>
        [JsonProperty("venue")]
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                OnPropertyChanged("Venue");
            }
        }

        private string message;
        private SpecialType type;
        private string kind;
        private Venue venue;
    }
}
