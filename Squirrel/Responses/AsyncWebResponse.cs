using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Squirrel.Abstraction;
using Squirrel.Domain.Base;

namespace Squirrel
{
    /// <summary>
    /// Container for async http response.
    /// </summary>
    /// <typeparam name="T">Target object type.</typeparam>
    public class AsyncWebResponse<T> : AsyncResponse<T>
    {
        internal AsyncWebResponse(HttpWebRequest request)
        {
            this.request = request;
        }

        internal HttpWebRequest Request
        {
            get
            {
                return request;
            }
        }

        internal void Raise(T data)
        {
            if (OnCompleted != null)
            {
                OnCompleted(this, new FourSquareEventArgs<T>(data));
            }
        }

        private HttpWebRequest request;
    }
}
