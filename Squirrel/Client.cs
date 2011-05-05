using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
