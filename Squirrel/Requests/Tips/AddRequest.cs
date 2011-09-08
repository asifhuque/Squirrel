using System;
using System.Net;
using Squirrel.Attributes;
using Squirrel.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Squirrel.Requests.Tips
{
    [RequestMethod("tips/add"), Version(EndPointVersion.V2)]
    public class AddRequest : Request
    {
        /// <summary>
        /// Gets or set the venue where you want to add this tip.
        /// </summary>
        [RequestProperty("venueId"), Required("Must provide a valid venue Id")]
        public string VenueId { get; set; }

        /// <summary>
        /// Gets or sets the text for the tip.
        /// </summary>
        [RequestProperty("text"), Required("Must provide a valid text to be used as tip")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or set the url related to this tip.
        /// </summary>
        [RequestProperty("url")]
        public string Url { get; set; }


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
       
                return string.Join(",", parts.ToArray()).ToLower();
            }
        }

        public override HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            return Create(proxy, HttpRequestMethod.GET);
        }

        public override HttpWebRequest Create(IHttpRequestProxy proxy, string method)
        {
            return Create(this, proxy, method);
        }
    }
}
