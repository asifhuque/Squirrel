
using Newtonsoft.Json;
using System;
using Arthus.Domain;
namespace Arthus
{
    public class CheckInResponse : ResponseObject
    {
        /// <summary>
        ///  Get or sets id for the check-in
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
        ///  Get or sets id for the check-in
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
        ///  Get or sets id for the check-in
        /// </summary>
        [JsonProperty("created")]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set
            {
                createdOn = value;
                OnPropertyChanged("CreatedOn");
            }
        }

        /// <summary>
        ///  Get or sets id for the check-in
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

        /// <summary>
        ///  Get or sets id for the check-in
        /// </summary>
        [JsonProperty("mayor")]
        public Mayor Mayor
        {
            get { return mayor; }
            set
            {
                mayor = value;
                OnPropertyChanged("Mayor");
            }
        }

        private Venue venue;
        private int id;
        private string message;
        private DateTime createdOn;
        private Mayor mayor;

    }
}
