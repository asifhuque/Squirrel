using Squirrel.Attributes;
using Squirrel.Abstraction;
using System.Net;

namespace Squirrel
{
    [RequestMethod("venues.json")]
    public class VenuesRequest : Request
    {
        internal VenuesRequest()
        {
            Limit = 10;
        }

        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        [RequestProperty("geolat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        [RequestProperty("geolong")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the matching venue text.
        /// </summary>
        [RequestProperty("q")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the number of venues to include in the search.
        /// </summary>
        [RequestProperty("l")]
        public int Limit { get; set; }


        #region IRequestUrl Members

        public HttpWebRequest Create(IHttpRequestProxy proxy)
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
