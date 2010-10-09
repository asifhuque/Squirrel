using System;
using Maven.Abstraction;

namespace Maven
{
    /// <summary>
    /// Contains the reponse data.
    /// </summary>
    public class FourSquareEventArgs : EventArgs
    {
        public FourSquareEventArgs(IResponse data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        public IResponse Data
        {
            get
            {
                return data;
            }
        }

        private IResponse data;
    }
}
