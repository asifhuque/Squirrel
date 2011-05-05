using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Squirrel.Domain.Base;
using Newtonsoft.Json;

namespace Squirrel.Domain
{
    /// <summary>
    /// Defines the location for the associated venue.
    /// </summary>
    public class Location : ResponseObject
    {
        /// <summary>
        ///  Get or sets address
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
        ///  Get or sets cross street
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
        ///  Get or sets city
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
        ///  Get or sets state
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
        ///  Get or sets the postal code
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode
        {
            get { return postalcode; }
            set
            {
                postalcode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        /// <summary>
        ///  Get or sets the latitude
        /// </summary>
        [JsonProperty("lat")]
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
        ///  Get or sets the longitude
        /// </summary>
        [JsonProperty("lng")]
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
        ///  Get or sets distance (Km) of the location
        /// </summary>
        [JsonProperty("distance")]
        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }


        /// <summary>
        /// Gets the full adress of the venue.
        /// </summary>
        public string FullAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(City))
                {
                    return string.Format("{0}, {1}", Address, City);
                }
                return Address;
            }
        }

        private string address;
        private string crossstreet;
        private string city;
        private string state;
        private string postalcode;
        private double latitude;
        private double longitude;
        private double distance;


    }
}
