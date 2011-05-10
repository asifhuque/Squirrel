using System;
using System.Threading;

namespace Squirrel
{
    internal sealed class FourSquareAsyncResult : IAsyncResult
    {
        internal FourSquareAsyncResult(object state)
        {
            this.state = state;
        }
        object IAsyncResult.AsyncState
        {
            get
            {
                return state;
            }
        }
        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        bool IAsyncResult.CompletedSynchronously
        {
            get
            {
                return false;
            }
        }
        bool IAsyncResult.IsCompleted
        {
            get
            {
                return true;
            }
        }
        private readonly object state;
    }
}
