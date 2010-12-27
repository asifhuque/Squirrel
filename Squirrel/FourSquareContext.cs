using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Squirrel.Abstraction;
using Squirrel.Domain.Base;
using Squirrel.Proxy;

namespace Squirrel
{
    /// <summary>
    /// Entry point class for interacting with foursquare.
    /// </summary>
    public class FourSquareContext
    {
        public FourSquareContext(IHttpRequestProxy request, bool async)
        {
            this.httpRequest = request;
            this.async = async;
        }

        public FourSquareContext()
            : this(new HttpRequestProxy(), true)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(string username, string password)
            : this(username, password, true)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(bool async)
            : this(string.Empty, string.Empty, async)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(string username, string password, bool async)
            : this(new HttpRequestProxy(), async)
        {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Sets credential to current context.
        /// </summary>
        public void SetCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
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
        /// Searches a group of venues for a specific location
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public VenueGroupReponse FindVenues(double latitude, double longitude)
        {
            return FindVenues(string.Empty, latitude, longitude);
        }

        /// <summary>
        /// Searches a group of venues for a specific location and text
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public VenueGroupReponse FindVenues(string text, double latitude, double longitude)
        {
            this.async = false;
            VenueGroupReponse result = null;

            var response = this.BeginFindVenues(string.Empty, latitude, longitude);
                
            response.OnCompleted += (sender, args) =>
            {
                result = args.Data;
            };


            this.EndFindVenues(response);

            return result;
        }

        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public AsyncWebResponse<VenueGroupReponse> BeginFindVenues(double latitude, double longitude)
        {
            return BeginFindVenues(string.Empty, latitude, longitude);
        }

        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="text">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public AsyncWebResponse<VenueGroupReponse> BeginFindVenues(string text, double latitude, double longitude)
        {
            var vRequest = new VenuesRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Text = text,
                Limit = 50
            };

            return new AsyncWebResponse<VenueGroupReponse>(vRequest.Create(httpRequest));
        }

        public void EndFindVenues(AsyncResponse<VenueGroupReponse> response)
        {
            var venues = response as AsyncWebResponse<VenueGroupReponse>;
            ProcessRequestAsync<VenueGroupReponse>(venues.Request, r => venues.Raise(r));
        }

        /// <summary>
        /// Gets a venue for a specific venue id.
        /// </summary>
        /// <param name="venueId">Target venue id</param>
        public VenueResponse GetVenue(int venueId)
        {
            this.async = false;
            VenueResponse result = null;
            var async = BeginGetVenue(venueId);

            async.OnCompleted += (sender, args) =>
            {
                result = args.Data;
            };

            EndGetVenue(async);

            return result;
        }

        /// <summary>
        /// Gets a venue for a specific venue id.
        /// </summary>
        /// <param name="venueId">Target venue id</param>
        public AsyncWebResponse<VenueResponse> BeginGetVenue(int venueId)
        {
            var vRequest = new VenueRequest { VenueId = venueId };
            return new AsyncWebResponse<VenueResponse>(vRequest.Create(httpRequest));
        }

        public  void EndGetVenue(AsyncResponse<VenueResponse> response)
        {
            var result = response as AsyncWebResponse<VenueResponse>;
            ProcessRequestAsync<VenueResponse>(result.Request, r => result.Raise(r));
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
                        string responseText = httpRequest.GetResponse(req, result);
                        obj = ProcessResponse<T>(responseText);
                        var ret = func(obj);
                        if (!string.IsNullOrEmpty(ret.Error))
                            throw new FourSquareException(ret.Error);
                        action(ret);
                    }
                    catch (Exception ex)
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
            T obj;

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
                obj = serializer.Deserialize<T>(new JsonTextReader(reader));
            }

            return obj;
        }

        bool HasCredientials()
        {
            return string.IsNullOrEmpty(this.username) && !string.IsNullOrEmpty(this.password);
        }

        #endregion

        private IHttpRequestProxy httpRequest;

        private bool async;
        private string username;
        private string password;

        private Exception exception;
    }
}
