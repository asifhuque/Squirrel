using Squirrel.Domain.Base;
using Squirrel.Abstraction;
using System;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Phone.Reactive;

namespace Squirrel.Services
{
    public abstract class Service
    {
        protected Service(IHttpRequestProxy httpRequest, string token)
        {
            this.httpRequest = httpRequest;
            this.token = token;
        }


        protected Func<TRequest, IObservable<TResponse>> CreateObservablePattern<TRequest, TResponse>()
            where TRequest : Request
            where TResponse : ResponseObject
        {
            return CreateObservablePattern<TRequest, TResponse>(HttpRequestMethod.GET);
        }

        protected  Func<TRequest, IObservable<TResponse>> CreateObservablePattern<TRequest, TResponse>(string method) where TRequest : Request where TResponse : ResponseObject
        {
            this.httpRequest.Method = method;
            return Observable.FromAsyncPattern<TRequest, TResponse>(BeginAsync<TRequest, TResponse>, EndAsync<TResponse>);
        }

        protected IAsyncResult BeginAsync<T, TRet>(T request, AsyncCallback callback, object state)
            where T : Request
            where TRet : ResponseObject
        {
            request.OAuthToken = token;

            var method = !string.IsNullOrEmpty(httpRequest.Method) ? httpRequest.Method : HttpRequestMethod.GET;
            

            var req = request.Create(httpRequest, method);

            return httpRequest.BeginGetResponse(req, result =>
            {
                try
                {
                    exception = null;
                    string responseText = httpRequest.GetResponse(req, result);

                    callback(new FourSquareAsyncResult(ProcessResponse<TRet>(responseText)));
                }
                catch (FourSquareException ex)
                {
                    exception = ex;
                }

            }, state);
        }

        protected T EndAsync<T>(IAsyncResult result) where T : ResponseObject
        {
            if (result.IsCompleted)
            {
                return (T)result.AsyncState;
            }

            return default(T);
        }


        T ProcessResponse<T>(string responseString) where T : ResponseObject
        {
            Responses.FourSquareResponse<T> response;

            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Include,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DefaultValueHandling = DefaultValueHandling.Include
            };

            using (var reader = new StringReader(responseString))
            {
                if (responseString.Contains("response"))
                {
                    response = serializer.Deserialize<Responses.FourSquareResponse<T>>(new JsonTextReader(reader));
                    return response.Response;
                }
                return serializer.Deserialize<T>(new JsonTextReader(reader));
            }
        }


        private readonly IHttpRequestProxy httpRequest;
        private Exception exception;
        private string token;

    }
}
