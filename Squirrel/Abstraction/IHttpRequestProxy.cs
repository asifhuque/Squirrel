using System.Threading;
using System.Net;
using System;

namespace Squirrel.Abstraction
{
    public interface IHttpRequestProxy
    {
        /// <summary>
        /// Creates a web request from a specific url.
        /// </summary>
        /// <param name="url">Target url.</param>
        /// <returns>Web request instance.</returns>
        HttpWebRequest Create(string url);

        /// <summary>
        /// Gets response text from the specified request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <returns>Response from server.</returns>
        string GetResponse(HttpWebRequest request, IAsyncResult result);

        /// <summary>
        /// Begins the get response process.
        /// </summary>
        /// <param name="request">Http request object</param>
        /// <param name="callback">Target callback</param>
        /// <param name="state">Target object instance</param>
        IAsyncResult BeginGetResponse(HttpWebRequest request, AsyncCallback callback, object state);
    }
}
