using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squirrel
{
    internal class NameValuePair
    {
        /// <summary>
        /// Gets or sets the name of reqeust param
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the request param.
        /// </summary>
        public string Value { get; set; }
    }
}
