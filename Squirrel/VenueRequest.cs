using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirrel.Abstraction;
using Squirrel.Attributes;

namespace Squirrel
{
    [RequestMethod("venue.json")]
    public class VenueRequest : IUrlProcessor
    {
        /// <summary>
        /// Gets or sets Id for the venue.
        /// </summary>
        [RequestProperty("vid")]
        public int Id { get; set; }

        #region IUrlProcessor Members

        public string GetUrl()
        {
            return UrlProcessorFactory.Process(this);
        }

        #endregion
    }
}
