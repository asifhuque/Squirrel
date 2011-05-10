using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Squirrel.Abstraction;
using Squirrel.Domain.Base;
using Squirrel.Proxy;
using Microsoft.Phone.Reactive;

namespace Squirrel
{
    /// <summary>
    /// Entry point class for interacting with foursquare.
    /// </summary>
    public class FourSquareContext : IVenueContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="FourSquareContext"/> class.
        /// </summary>
        public FourSquareContext()
            : this(new HttpRequestProxy())
        {
            // intentionally left blank.   
        }

        /// <summary>
        /// Initializes new instance of the <see cref="FourSquareContext"/> class.
        /// </summary>
        /// <param name="request">Target request proxy.</param>
        public FourSquareContext(IHttpRequestProxy request)
        {
            this.httpRequest = request;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="FourSquareContext"/> class.
        /// </summary>
        /// <param name="username">Target username</param>
        /// <param name="password">Target password</param>
        public FourSquareContext(string username, string password)
            : this(new HttpRequestProxy())
        {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Gets the current venue context.
        /// </summary>
        public IVenueContext Venues
        {
            get
            {
                return this as IVenueContext;
            }
        }

        /// <summary>
        /// Checks in to a specific venue or location.
        /// </summary>
        /// <param name="checkInrequest">Reqeust to check-in</param>
        public void CheckIn(CheckInRequest checkInrequest, Action<CheckInResponse> response)
        {
            var req = checkInrequest.Create(httpRequest);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            // must be a post method.
            req.Method = HttpRequestMethod.POST;

            ProcessRequestAsync<CheckInResponse>(req, response);
        }

        /// <summary>
        /// Searches a group of venues for a specific location and text
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        IObservable<VenueResponse>  IVenueContext.Search(string text, double latitude, double longitude)
        {
            var vRequest = new VenueRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Text = text,
                Limit = 50
            };

            var func = Observable.FromAsyncPattern<VenueRequest, VenueResponse> (this.BeginAsync<VenueRequest, VenueResponse>, this.EndAsync<VenueResponse>);
            
            return func(vRequest);
        }


        IAsyncResult BeginAsync<T, TRet>(T request, AsyncCallback callback, object state) where T : Request where TRet : ResponseObject
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

        T EndAsync<T>(IAsyncResult result) where T : ResponseObject
        {
            if (result.IsCompleted)
            {
                return (T)result.AsyncState;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the specific user.
        /// </summary>
        public UserResponse GetUser(int userId)
        {
            this.async = false;
            UserResponse result = null;
            var async = BeginGetUser(userId);

            async.OnCompleted += (sender, args) =>
            {
                result = args.Data;
            };

            EndGetUser(async);

            return result;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public AsyncWebResponse<UserResponse> BeginGetUser()
        {
            return BeginGetUser(0);
        }

        /// <summary>
        /// Gets the specific user.
        /// </summary>
        public AsyncWebResponse<UserResponse> BeginGetUser(int userId)
        {
            return BeginGetUser(userId, false);
        }

        /// <summary>
        /// Authenticates the specific user and optionally inlcudes mayor and badges detail in response.
        /// </summary>
        public AsyncWebResponse<UserResponse> BeginGetUser(int userId, bool inlcudeDetailInResult)
        {
            UserRequest userRequest = new UserRequest
            {
                UserId = userId,
                Badges = inlcudeDetailInResult,
                Mayor = inlcudeDetailInResult
            };

            var request = userRequest.Create(httpRequest);

            // must be authorized.
            AuthorizeRequest(request, username, password);

            return new AsyncWebResponse<UserResponse>(request);
        }

        public void EndGetUser(AsyncResponse<UserResponse> response)
        {
            var result = response as AsyncWebResponse<UserResponse>;
            ProcessRequestAsync<UserResponse>(result.Request, r => result.Raise(r));
        }

        /// <summary>
        /// Gets a specific user.
        /// </summary>
        /// <param name="userId">User id for the user</param>
        /// <param name="badges">Specifies whether to include badges</param>
        /// <param name="mayor">Specifies whether to include mayor tag</param>
        public void GetUser(int userId, bool badges, bool mayor, Action<UserResponse> response)
        {
            UserRequest userRequest = new UserRequest
            {
                UserId = userId,
                Badges = badges,
                Mayor = mayor
            };

            var req = userRequest.Create(httpRequest);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            ProcessRequestAsync<UserResponse>(req, response);

            // TODO : raise error and add event handler here.
        }

        /// <summary>
        /// Gets the predefined categories and their sub-categories.
        /// </summary>
        public CategoryResponse GetCategories()
        {
            this.async = false;
            CategoryResponse result = null;
            var async = BeginGetCategories();

            async.OnCompleted += (sender, args) =>
            {
                result = args.Data;
            };

            EndGetCategories(async);

            return result;
        }

        /// <summary>
        /// Gets the predefined categories and their sub-categories.
        /// </summary>
        public AsyncWebResponse<CategoryResponse> BeginGetCategories()
        {
            var catRequest = new CategoryRequest();
            return new AsyncWebResponse<CategoryResponse>(catRequest.Create(httpRequest));
        }

        public void EndGetCategories(AsyncResponse<CategoryResponse> response)
        {
            var result = response as AsyncWebResponse<CategoryResponse>;
            ProcessRequestAsync<CategoryResponse>(result.Request, r => result.Raise(r));
        }

        #region Private methods

        void ProcessRequestAsync<T>(HttpWebRequest req, Action<T> action) where T : ResponseObject
        {
            ProcessRequestAsync<T, T>(req, (x) => x, action);
            // reset
            this.async = true;
        }

        void ProcessRequestAsync<T, TRet>(HttpWebRequest req, Func<T, TRet> func, Action<TRet> action)
            where T : ResponseObject
            where TRet : ResponseObject
        {
            T obj = Activator.CreateInstance<T>();

            using (var resetEvent = new AutoResetEvent(false))
            {
                httpRequest.BeginGetResponse(req, result =>
                {
                    try
                    {
                        exception = null;

                        string responseText = httpRequest.GetResponse(req, result);
                        
                        obj = ProcessResponse<T>(responseText);
                        
                        var ret = func(obj);
                        
                        if (!string.IsNullOrEmpty(ret.Error))
                            throw new FourSquareException(ret.Error);
                        
                        if (action != null)
                            action(ret);
                    }
                    catch (FourSquareException ex)
                    {
                        exception = ex;
                    }
                    
                    if (!async)
                    {
                        ((AutoResetEvent)result.AsyncState).Set();
                    }

                }, resetEvent);
               
                if (!async)
                {
                    resetEvent.WaitOne();
                    resetEvent.Close();
                }
            }

            // TODO : Add event handler.
            if (exception != null)
            {
                throw new FourSquareException(exception.Message);
            }
        }

        void AuthorizeRequest(HttpWebRequest req, string username, string password)
        {
            string enHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));

            req.Headers["Authorization"] = string.Format("Basic {0}", enHeader);
            req.Credentials = new NetworkCredential(username, password);
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

        #endregion
        private bool async;
        private string username;
        private string password;

        private Exception exception;

        private readonly IHttpRequestProxy httpRequest;

    }
}