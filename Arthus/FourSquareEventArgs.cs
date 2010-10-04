using System;

namespace Arthus
{
    public class FourSquareEventArgs : EventArgs
    {
        public FourSquareEventArgs(object data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        public object Data
        {
            get
            {
                return data;
            }
        }

        private object data;
    }
}
