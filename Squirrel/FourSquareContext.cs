using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Squirrel.Abstraction;
using Squirrel.Domain.Base;

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

        public FourSquareContext() : this(new HttpRequestProxy(), true)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(string username, string password) : this(username, password, true)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(bool async)
            : this(string.Empty, string.Empty, async)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(string username, string password, bool async) : this(new HttpRequestProxy(), async)
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
        /// <param name="request">Reqeust to check-in</param>
        public void CheckIn(CheckInRequest checkInrequest, Action<CheckInResponse> response)
        {
            string url = checkInrequest.GetUrl();
            HttpWebRequest req = httpRequest.Create(url);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            // must be a post method.
            req.Method = HttpRequestMethod.POST;

            ProcessRequestAsync<CheckInResponse>(req, response);
        }

        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="key">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public void FindVenues(double latitude, double longitude, Action<VenuesResponse> response)
        {
            FindVenues(string.Empty, latitude, longitude, response);
        }
            
        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="text">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public void FindVenues(string text, double latitude, double longitude, Action<VenuesResponse> response)
        {
            VenuesRequest vRequest = new VenuesRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Text = text,
                Limit = 50
            };
            ProcessRequestAsync<VenuesResponse>(httpRequest.Create(vRequest.GetUrl()), response);
        }

        /// <summary>
        /// Gets a venue for a specific vid.
        /// </summary>
        /// <param name="venueId">Target venue id</param>
        /// <param name="venueResponse">Response containing venue object.</param>
        public void GetVenue(int venueId, Action<VenueResponse> venueResponse)
        {
            var vRequest = new VenueRequest { Id = venueId };

            HttpWebRequest req = httpRequest.Create(vRequest.GetUrl());

            if (HasCredientials())
            {
                // must be authorized.
                AuthorizeRequest(req, username, password);
            }

            ProcessRequestAsync<VenueResponse>(req, venueResponse);
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public void GetCurrentUser(Action<UserResponse> response)
        {
           GetUser(0, false, false, response);
        }


        /// <summary>
        /// Gets the current user.
        /// </summary>
        public void GetCurrentUser(bool badges, bool mayor, Action<UserResponse> response)
        {
            GetUser(0, badges, mayor, response);
        }
      
        /// <summary>
        /// Gets a specific user.
        /// </summary>
        /// <param name="userId">User id for the user</param>
        /// <param name="twitterId">Twitter id for the user.</param>
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

            string url = userRequest.GetUrl();

            HttpWebRequest req = httpRequest.Create(url);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            ProcessRequestAsync<UserResponse>(req, response);

            // TODO : raise error and add event handler here.
        }


        /// <summary>
        /// Gets the predefined categories and their sub-categories.
        /// </summary>
        public void GetCategories(Action<CategoryResponse> response)
        {
            var catRequest = new CategoryRequest();
            ProcessRequestAsync<CategoryResponse>(httpRequest.Create(catRequest.GetUrl()), response);
        }

        #region Private methods

        private void ProcessRequestAsync<T>(HttpWebRequest req, Action<T> action) where T : ResponseObject
        {
            ProcessRequestAsync<T, T>(req, (x) => x, action);
        }

        private void ProcessRequestAsync<T, TRet>(HttpWebRequest req, Func<T, TRet> func, Action<TRet> action)
            where T : ResponseObject
            where TRet : ResponseObject
        {
            var resetEvent = new AutoResetEvent(false);

            T obj = Activator.CreateInstance<T>();
          
            httpRequest.BeginGetResponse(req, delegate(IAsyncResult result)
            {
                try
                {
                    string responseText = httpRequest.GetResponse(req, result);

                    obj = ProcessResponse<T>(responseText);
                    TRet ret = func(obj);

                    if (!string.IsNullOrEmpty(ret.Error))
                    {
                        throw new FourSquareException(ret.Error);
                    }

                    action(ret);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                ((AutoResetEvent)result.AsyncState).Set();

            }, resetEvent);

            if (!async)
            {
                resetEvent.WaitOne();
                resetEvent.Close();
            }

            // TODO : Add event handler.
            if (exception != null)
            {
                throw new FourSquareException(exception.Message);
            }
        }

        private void AuthorizeRequest(HttpWebRequest req, string username, string password)
        {
            string enHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));

            req.Headers["Authorization"] = string.Format("Basic {0}", enHeader);
            req.Credentials = new NetworkCredential(username, password);
        }

        private T ProcessResponse<T>(string responseString) where T : ResponseObject
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

        private bool HasCredientials()
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
