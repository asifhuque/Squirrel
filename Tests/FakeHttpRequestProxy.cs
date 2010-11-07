using Squirrel.Abstraction;
using System;
using System.Net;

namespace Squirrel.Tests
{
    public class FakeHttpRequestProxy : IHttpRequestProxy
    {
        public FakeHttpRequestProxy(string response)
        {
            this.response = response;
        }

        #region IHttpRequest Members

        public HttpWebRequest Create(string url)
        {
            return null;
        }

        public string GetResponse(System.Net.HttpWebRequest request, IAsyncResult result)
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
