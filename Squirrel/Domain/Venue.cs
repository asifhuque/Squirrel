﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Venue : ResponseObject
    {
        /// <summary>
        ///  Get or sets id of the venue
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
        ///  Get or sets category of the venue
        /// </summary>
        [JsonProperty("primarycategory")]
        public PrimaryCategory Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        /// <summary>
        ///  Get or sets address of the venue
        /// </summary>
        [JsonProperty("address")]
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        /// <summary>
        ///  Get or sets cross street of the venue
        /// </summary>
        [JsonProperty("crossstreet")]
        public string CrossStreet
        {
            get { return crossstreet; }
            set
            {
                crossstreet = value;
                OnPropertyChanged("CrossStreet");
            }
        }

        /// <summary>
        ///  Get or sets city of the venue
        /// </summary>
        [JsonProperty("city")]
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        /// <summary>
        ///  Get or sets state of the venue
        /// </summary>
        [JsonProperty("state")]
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }

        /// <summary>
        ///  Get or sets id of the venue
        /// </summary>
        [JsonProperty("zip")]
        public string Zip
        {
            get { return zip; }
            set
            {
                zip = value;
                OnPropertyChanged("Zip");
            }
        }

        /// <summary>
        ///  Get or sets if the venue is verified.
        /// </summary>
        [JsonProperty("verified")]
        public bool Verfied
        {
            get { return verified; }
            set
            {
                verified = value;
                OnPropertyChanged("Verified");
            }
        }

        /// <summary>
        ///  Get or sets latitude of the venue
        /// </summary>
        [JsonProperty("geolat")]
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        /// <summary>
        ///  Get or sets longitude of the venue
        /// </summary>
        [JsonProperty("geolong")]
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        /// <summary>
        ///  Get or sets phone number of the venue
        /// </summary>
        [JsonProperty("stats")]
        public Stats Stats
        {
            get { return stats; }
            set
            {
                stats = value;
                OnPropertyChanged("Stats");
            }
        }

        /// <summary>
        ///  Get or sets the tips for the venue.
        /// </summary>
        [JsonProperty("tips")]
        public IList<Tip> Tips
        {
            get { return tips; }
            set
            {
                tips = value;
                OnPropertyChanged("Tips");
            }
        }

        /// <summary>
        ///  Get or set the specials associated with this venue.
        /// </summary>
        [JsonProperty("specials")]
        public IList<Special> Specials
        {
            get { return specials; }
            set
            {
                specials = value;
                OnPropertyChanged("Specials");
            }
        }


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

        /// <summary>
        ///  Get or sets the twitter id of the venue
        /// </summary>
        [JsonProperty("twitter")]
        public string Twitter
        {
            get { return twitter; }
            set
            {
                twitter = value;
                OnPropertyChanged("Twitter");
            }
        }

        /// <summary>
        ///  Get or sets distance (Km) of the venue
        /// </summary>
        [JsonProperty("distance")]
        public string Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string name;
        private string address;
        private string crossstreet;
        private string city;
        private string state;
        private string zip;
        private bool verified;
        private double latitude;
        private double longitude;
        private Stats stats;
        private string phone;
        private string twitter;
        private string distance;

        private PrimaryCategory category = new PrimaryCategory { IconUrl = "http://foursquare.com/img/categories/none.png" };
        private IList<Special> specials = new List<Special>();
        private IList<Tip> tips = new List<Tip>();

    }
}
