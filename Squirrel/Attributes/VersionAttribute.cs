using System;
using System.Net;

namespace Squirrel.Attributes
{
    internal enum  EndPointVersion
    {
        V1,
        V2
    }

    internal class VersionAttribute : System.Attribute
    {
        /// <summary>
        /// Initalizes a new instance of the <see cref="VersionAttribute"/> class.
        /// </summary>
        /// <param name="version">Marks endpoint version of the api</param>
        public VersionAttribute(EndPointVersion version)
        {
            this.legacy = version == EndPointVersion.V1;
        }

        /// <summary>
        /// Gets a value indicating if the legacy mode to be used.
        /// </summary>
        public bool IsLegacy
        {
            get
            {
                return legacy;
            }
        }

        private bool legacy = true;
    }
}
