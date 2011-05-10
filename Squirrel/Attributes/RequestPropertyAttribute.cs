using System;

namespace Squirrel.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Property)]
    internal class RequestPropertyAttribute : System.Attribute
    {
        public RequestPropertyAttribute(string elementName)
        {
            this.elementName = elementName;
            this.encode = true;
        }

        public RequestPropertyAttribute(string elementName, bool encode) : this(elementName)
        {
            this.encode = encode;
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

        /// <summary>
        /// Gets a value indicating whether to encode the request.
        /// </summary>
        public bool Encode
        {
            get { return encode; }
        }

        private readonly string elementName;
        private readonly bool encode;

    }
}
