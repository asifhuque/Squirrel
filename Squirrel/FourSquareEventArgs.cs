using System;

namespace Squirrel
{
    public class FourSquareEventArgs<T> : EventArgs
    {
        public FourSquareEventArgs(T data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public T Data
        {
            get
            {
                return data;
            }
        }

        private readonly T data;
    }
}
