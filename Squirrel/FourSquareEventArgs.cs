using System;
using Squirrel.Abstraction;

namespace Squirrel
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
