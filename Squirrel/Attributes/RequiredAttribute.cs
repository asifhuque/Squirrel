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

namespace Squirrel.Attributes
{
    /// <summary>
    /// Marks an element as requried 
    /// </summary>
    public class RequiredAttribute : System.Attribute
    {
        public RequiredAttribute(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Returns the message associated with the requried field.
        /// </summary>
        internal string Message
        {
            get
            {
                return message;
            }
        }

        private string message;
    }
}
