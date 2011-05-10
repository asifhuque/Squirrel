using Squirrel.Abstraction;
using Squirrel.Attributes;
using System.Net;

namespace Squirrel
{
    /// <summary>
    /// Defines the check-in request.
    /// </summary>
    [RequestMethod("checkin.json")]
    public class CheckInRequest : Request
    {
        /// <summary>
        /// Gets or sets if the check-in should be pushed to twitter.
        /// </summary>
        [RequestProperty("twitter")]
        public bool PushToTwitter {get;set;}
        
        /// <summary>
        /// Gets or sets if the check-in should be pushed to facebook.
        /// </summary>
        [RequestProperty("facebook")]
        public bool PushToFacebook {get;set;}
       
        /// <summary>
        /// Gets or set if the check-in is private.
        /// </summary>
        [RequestProperty("private")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the venue id.
        /// </summary>
        [RequestProperty("vid")]
        public int VenueId { get; set; }

        /// <summary>
        /// Gets or sets the venue name.
        /// </summary>
        [RequestProperty("venue")]
        public string Venue { get; set; }

        /// <summary>
        /// Gets or sets the shout text in 140 charecter.
        /// </summary>
        [RequestProperty("shout")]
        public string Shout { get; set; }

        /// <summary>
        /// Gets or sets the latitude for the check-in
        /// </summary>
        [RequestProperty("geolat")]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude for the check-in
        /// </summary>
        [RequestProperty("geolong")]
        public string Longitude { get; set; }


        #region IUrlProcessor Members

        public override HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            if (VenueId == 0)
            {
                if (string.IsNullOrEmpty(Shout) && string.IsNullOrEmpty(Venue))
                {
                    throw new FourSquareException("Must have a venue id");
                }
                else if (!string.IsNullOrEmpty(Venue))
                {
                    if (string.IsNullOrEmpty(Latitude) || string.IsNullOrEmpty(Longitude))
                    {
                        throw new FourSquareException("Must provide a latidue and longitude to make a check-in into an orphan venue");
                    }
                }
            }

            if (!string.IsNullOrEmpty(Shout) && Shout.Length > 140)
            {
                throw new FourSquareException("Shout text must be within 140 charecters");
            }
            
            return Create(this, proxy);
        }

        #endregion
    }
}
