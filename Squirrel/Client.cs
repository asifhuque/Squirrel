using System;

namespace Squirrel
{
    /// <summary>
    /// Defines the client credentials
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Set your client id associated with foursquare
        /// </summary>
        public static string Key
        {
            internal get;
            set;
        }

        /// <summary>
        /// Set your client secrect associated with foursquare
        /// </summary>
        public static string Secret
        {
            internal get;
            set;
        }

        /// <summary>
        /// Set your registered url with foursquare
        /// </summary>
        public static string RedirectUrl
        {
            internal get;
            set;
        }
    }
}
