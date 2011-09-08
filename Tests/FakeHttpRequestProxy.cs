using Squirrel.Abstraction;
using System;
using System.Net;
using Squirrel.Proxy;

namespace Squirrel.Tests
{
    public class FakeHttpRequestProxy : IHttpRequestProxy
    {
        public FakeHttpRequestProxy(string response)
        {
            this.response = response;
        }

        public string Method { get; set; }

        public HttpWebRequest Request { get; set; }

        #region IHttpRequest Members

        public HttpWebRequest Create(string url)
        {
            return Create(url, HttpRequestMethod.GET);
        }

        public HttpWebRequest Create(string url, string method)
        {
            Request = new HttpRequestProxy().Create(url, method);
            return Request;
        }

        public string GetResponse(HttpWebRequest request, IAsyncResult result)
        {
            return response;
        }

        public IAsyncResult BeginGetResponse(HttpWebRequest request, AsyncCallback callback, object state)
        {
            var result = new FakeAsyncResult(state);
            callback(result);
            return result;
        }

        #endregion

        private string response = string.Empty;
    }

    

}
