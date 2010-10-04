using System;

namespace Arthus
{
    public class FourSquareEventArgs<T> : EventArgs
    {
        public FourSquareEventArgs(T data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        public T Data
        {
            get
            {
                return data;
            }
        }

        private T data;
    }
}
