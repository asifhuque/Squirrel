using System;

namespace Squirrel.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Property)]
    internal class RequestPropertyAttribute : System.Attribute
    {
        private string elementName;

        public RequestPropertyAttribute(string elementName)
        {
            this.elementName = elementName;
        }

        /// <summary>
        /// Gets the element name.
        /// </summary>
        public string ElementName
        {
            get
            {
                return elementName;
            }
        }

    }
}
