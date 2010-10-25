using Squirrel.Attributes;
using Squirrel.Abstraction;

namespace Squirrel
{
    [RequestMethod("venues.json")]
    public class VenueRequest : IRequestUrl
    {
        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        [RequestProperty("geolat")]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        [RequestProperty("geolong")]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the matching venue text.
        /// </summary>
        [RequestProperty("q")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the number of venues to include in the search.
        /// </summary>
        [RequestProperty("l")]
        public int Count { get; set; }


        #region IRequestUrl Members

        public string GetUrl()
        {
            return UrlBuilder.GetUrl(this);
        }

        #endregion
    }
}
