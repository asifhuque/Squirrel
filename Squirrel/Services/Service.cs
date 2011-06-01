
using Squirrel.Domain.Base;
using Squirrel.Abstraction;
using System;
using Newtonsoft.Json;
using System.IO;

namespace Squirrel.Services
{
    public abstract class Service
    {
        protected Service(IHttpRequestProxy httpRequest)
        {
            this.httpRequest = httpRequest;
        }

        protected IAsyncResult BeginAsync<T, TRet>(T request, AsyncCallback callback, object state)
            where T : Request
            where TRet : ResponseObject
        {
            var req = request.Create(httpRequest);

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
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            };

            using (var reader = new StringReader(responseString))
            {
                response = serializer.Deserialize<Responses.FourSquareResponse<T>>(new JsonTextReader(reader));
            }

            return response.Response;
        }


        private readonly IHttpRequestProxy httpRequest;
        private Exception exception;

    }
}
