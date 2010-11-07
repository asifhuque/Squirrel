using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirrel.Abstraction;
using System;

namespace Squirrel.Tests
{
    public class FakeAsyncResult : IAsyncResult
    {
        public FakeAsyncResult(object state)
        {
            this.state = state;
        }

        #region IAsyncResult Members

        public object AsyncState
        {
            get
            {
                return state;
            }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get
            {
                return null;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return true;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return true;
            }
        }

        #endregion

        private object state;
    }
}
