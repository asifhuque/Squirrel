using System;
using System.Net;
using Squirrel.Attributes;
using Squirrel.Abstraction;

namespace Squirrel.Requests.Tips
{
   [RequestMethod("tips/search"), Version(EndPointVersion.V2)]
    public class SearchRequest : Request
    {
        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        public double Longitude { get; set; }

        [RequestProperty("ll"), Required("Must provide both latitude and longitude")]
        internal string LatLong
        {
            get
            {
                if (Latitude == 0 || Longitude == 0)
                {
                    return string.Empty;
                }

                return string.Format("{0}, {1}", Latitude, Longitude);
            }
        }

       [RequestProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the number of venues to include in the search.
        /// </summary>
        [RequestProperty("limit")]
        public int Limit { get; set; }


        #region IRequestUrl Members

        public override HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            return Create(this, proxy);
        }

        #endregion
    }
}
