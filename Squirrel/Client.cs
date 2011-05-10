using System;

namespace Squirrel
{
    /// <summary>
    /// Defines the client credentials
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public static string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the client secret
        /// </summary>
        public static string Secret
        {
            get;
            set;
        }
    }
}
