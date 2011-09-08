using Squirrel.Abstraction;
using System.Net;
using System;
using System.IO;
using System.Text;

namespace Squirrel.Proxy
{
    internal class HttpRequestProxy : IHttpRequestProxy
    {
        #region IHttpRequest Members

        /// <summary>
        /// Gets or sets the request method. Ex. POST /GET.
        /// </summary>
        public string Method { get; set; }


        /// <summary>
        /// Creates a web request from a specific url.
        /// </summary>
        /// <param name="url">Target url.</param>
        /// <returns>Web request instance.</returns>
        public HttpWebRequest Create(string url)
        {
            return Create(url, HttpRequestMethod.GET);
        }

        /// <summary>
        /// Creates a web request from a specific url.
        /// </summary>
        /// <param name="url">Target url.</param>
        /// <returns>Web request instance.</returns>
        public HttpWebRequest Create(string url, string method)
        {
            HttpWebRequest req = null;
            if (method == HttpRequestMethod.POST)
            {
                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.ContentType = "application/json";
                req.Method = method;

            }
            else
            {
                req = (HttpWebRequest)HttpWebRequest.Create(url);
            }
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
        /// <param name="state">Target object instance</param>
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
