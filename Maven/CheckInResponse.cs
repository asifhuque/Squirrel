
using Newtonsoft.Json;
using System;
using Maven.Domain;
using System.Collections.Generic;
namespace Maven
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

        /// <summary>
        ///  Get or sets badges for the check-in
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

        /// <summary>
        ///  Get or sets scoring for the check-in
        /// </summary>
        [JsonProperty("scoring")]
        public IList<Score> Scoring
        {
            get { return scoring; }
            set
            {
                scoring = value;
                OnPropertyChanged("Scoring");
            }
        }

        private Venue venue;
        private int id;
        private string message;
        private DateTime createdOn;
        private Mayor mayor;

        private IList<Badge> badges = new List<Badge>();
        private IList<Score> scoring = new List<Score>();
    }
}
