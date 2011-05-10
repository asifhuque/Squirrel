using System;
using Squirrel.Domain.Base;
using Newtonsoft.Json;

namespace Squirrel.Domain
{
    public class Contact : ResponseObject
    {
        /// <summary>
        ///  Get or sets phone number of the venue
        /// </summary>
        [JsonProperty("phone")]
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        private string phone;

    }
}
