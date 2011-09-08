using Squirrel.Abstraction;
using Squirrel.Attributes;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Squirrel
{
    [Flags]
    public enum Broadcast
    {
        Private = 0x0,
        Public = 0x1,
        Facebook = 0x2,
        Twitter = 0x4
    }

    /// <summary>
    /// Defines the check-in request.
    /// </summary>
    [RequestMethod("checkins/add"), Version(EndPointVersion.V2)]
    public class CheckinRequest : Request
    {
        public Broadcast Broadcast { get; set; }

        [RequestProperty("broadcast")]
        internal string BroadcastText
        {
            get
            {
                IList<string> parts = new List<string>();

                if ((Broadcast & Broadcast.Facebook) == Broadcast.Facebook)
                    parts.Add(Broadcast.Facebook.ToString());
                if ((Broadcast & Broadcast.Twitter) == Broadcast.Twitter)
                    parts.Add(Broadcast.Twitter.ToString());
                if ((Broadcast & Broadcast.Public) == Broadcast.Public)
                    parts.Add(Broadcast.Public.ToString());
                if ((Broadcast & Broadcast.Private) == Broadcast.Private)
                    parts.Add(Broadcast.Private.ToString());
                if (parts.Count == 0)
                    parts.Add(Broadcast.Public.ToString());

                return string.Join(",", parts.ToArray()).ToLower();
            }
        }

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


        /// <summary>
        /// Gets or sets the venue id.
        /// </summary>
        [RequestProperty("venueId")]
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

      
        #region IUrlProcessor Members

        public override HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            return Create(proxy, HttpRequestMethod.GET);
        }

        public override HttpWebRequest Create(IHttpRequestProxy proxy, string method)
        {
            if (string.IsNullOrEmpty(Venue) && VenueId == 0)
            {
                throw new FourSquareException("Must provide a venue name or associated id");
            }

            if (!string.IsNullOrEmpty(Shout) && Shout.Length > 140)
            {
                throw new FourSquareException("Shout text must be within 140 charecters");
            }
            
            return Create(this, proxy, method);
        }

        #endregion
    }
}
