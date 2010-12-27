using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirrel.Abstraction;
using Squirrel.Attributes;
using System.Net;

namespace Squirrel
{
    /// <summary>
    /// Represents single venue request.
    /// </summary>
    [RequestMethod("venue.json")]
    public class VenueRequest : Request
    {
        /// <summary>
        /// Gets or sets Id for the venue.
        /// </summary>
        [RequestProperty("vid")]
        public int VenueId { get; set; }

        #region IUrlProcessor Members

        public HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            return Create(this, proxy);
        }

        #endregion
    }
}
