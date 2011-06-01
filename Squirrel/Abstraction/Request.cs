using System;
using System.Net;
using Squirrel.Attributes;

namespace Squirrel.Abstraction
{
    /// <summary>
    /// Base request entry-point.
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// Creates the web request for a target proxy.
        /// </summary>
        /// <param name="proxy">Target proxy</param>
        /// <returns><see cref="HttpWebRequest"/> instance</returns>
        public abstract HttpWebRequest Create(IHttpRequestProxy proxy);

        /// <summary>
        /// Creates the web request target request.
        /// </summary>
        /// <param name="target">Target request object</param>
        /// <param name="proxy">Target proxy</param>
        /// <returns>Expected web request</returns>
        internal HttpWebRequest Create(object target, IHttpRequestProxy proxy)
        {
            var processor = new EndpointProcessor(target);

            string url = processor.GetBaseUrl() + "?";

            foreach (var parameter in processor.GetParameters())
                url += string.Format("{0}={1}&", parameter.Name, parameter.Value);
           
            if (url.IndexOf('&') >= 0 || url[url.Length - 1] == '?')
                url = url.Substring(0, url.Length - 1);
            
            return proxy.Create(url);
        }


        /// <summary>
        /// Gets the client id.
        /// </summary>
        [RequestProperty("client_id")]
        public string Key
        {
            get { return Client.Key; }
        }

        /// <summary>
        /// Gets the client secret
        /// </summary>
        [RequestProperty("client_secret")]
        public string Secret
        {
            get { return Client.Secret; }
        }
    }
}
