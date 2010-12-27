using System;
using System.Net;

namespace Squirrel.Abstraction
{
    /// <summary>
    /// Base request entry-point.
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// Creates the web request target request.
        /// </summary>
        /// <param name="target">Target request object</param>
        /// <param name="proxy">Target proxy</param>
        /// <returns>Expected web request</returns>
        internal HttpWebRequest Create(object target, IHttpRequestProxy proxy)
        {
            var urlHelper = new WebUtility(target);

            string url = urlHelper.GetBaseUrl() + "?";

            foreach (var parameter in urlHelper.GetParameters())
                url += string.Format("{0}={1}&", parameter.Name, parameter.Value);
           
            if (url.IndexOf('&') >= 0 || url[url.Length - 1] == '?')
                url = url.Substring(0, url.Length - 1);
            
            return proxy.Create(url);
        }
    }
}
