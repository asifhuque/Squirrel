using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arthus.Attributes
{
    internal class RequestMethodAttribute : System.Attribute
    {
        public RequestMethodAttribute(string method)
        {
            this.method = method;
        }

        public string Method
        {
            get
            {
                return this.method;
            }
        }

        private string method;
    }
}
