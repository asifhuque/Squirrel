using Newtonsoft.Json;
using System.Collections.Generic;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Notification : ResponseObject
    {
        /// <summary>
        /// Gets or sets the type of the notification.
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
        /// Gets the flat item text object.
        /// </summary>
        [JsonProperty("item")]
        public Flat Item
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
        }

        private string type;
        private Flat item;

    }
}
