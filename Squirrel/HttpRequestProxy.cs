using Squirrel.Abstraction;
using System.Net;
using System;
using System.IO;

namespace Squirrel
{
    internal class HttpRequestProxy : IHttpRequestProxy
    {
        #region IHttpRequest Members

        /// <summary>
        /// Creates a web request from a specific url.
        /// </summary>
        /// <param name="url">Target url.</param>
        /// <returns>Web request instance.</returns>
        public HttpWebRequest Create(string url)
        {
            var req = (HttpWebRequest) HttpWebRequest.Create(url);
            req.UserAgent = "Squirrel:0.5.0 Guid/" + Guid.NewGuid().ToString("N");
            return req;
        }

        /// <summary>
        /// Gets response text from the specified request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <returns>Response from server.</returns>
        public string GetResponse(HttpWebRequest request, IAsyncResult result)
        {
            return ReadResponseString(request.EndGetResponse(result));
        }


        /// <summary>
        /// Begins the get response process.
        /// </summary>
        /// <param name="request">Http request object</param>
        /// <param name="callback">Target callback</param>
        /// <param name="statee">Target object instance</param>
        public IAsyncResult BeginGetResponse(HttpWebRequest request, AsyncCallback callback, object state)
        {
            return request.BeginGetResponse(callback, state);
        }

        #endregion

        private string ReadResponseString(WebResponse response)
        {
            string responseText;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseText = reader.ReadToEnd();
                }
            }

            return responseText;
        }
    }
}
