using System;
using Squirrel.Attributes;
using Squirrel.Abstraction;
using System.Net;

namespace Squirrel
{
    /// <summary>
    /// Contains attributes for requesting a venue.
    /// </summary>
    [RequestMethod("venues/search"), Version(EndPointVersion.V2)]
    public class VenueRequest : Request
    {
        internal VenueRequest()
        {
            Limit = 10;
        }

        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        public double Longitude { get; set; }

        [RequestProperty("ll")]
        internal string LatLong
        {
            get
            {
                if (Latitude == 0 || Longitude == 0)
                {
                    throw new ArgumentException("Must provide both latitude and longitude");
                }

                return string.Format("{0}, {1}", Latitude, Longitude);
            }
        }

        /// <summary>
        /// Gets or sets the matching venue text.
        /// </summary>
        [RequestProperty("query")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the number of venues to include in the search.
        /// </summary>
        [RequestProperty("limit")]
        public int Limit { get; set; }


        #region IRequestUrl Members

        public override HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            if (Limit > 50)
            {
                throw new FourSquareException("Default limit is 10 and maximum 50 is allowed.");
            }

            return Create(this, proxy);
        }

        #endregion
    }
}
