using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Maven.Abstraction;

namespace Maven
{
    /// <summary>
    /// Entry point class for interacting with foursquare.
    /// </summary>
    public class FourSquareContext
    {
        public FourSquareContext()
        {
            this.async = true;
            // intentionally left blank.   
        }

        public FourSquareContext(string username, string password) : this(username, password, true)
        {
            this.async = true;
            // intentionally left blank.   
        }

        public FourSquareContext(bool async)
            : this(string.Empty, string.Empty, async)
        {
            // intentionally left blank.   
        }

        public FourSquareContext(string username, string password, bool async)
        {
            this.username = username;
            this.password = password;
            this.async = async;
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
        public void CheckIn(CheckInRequest request)
        {
            string url = request.GetUrl();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            // must be a post method.
            req.Method = "POST";

            DoRequest<CheckInResponseContainer>(req);
        }

        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="key">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public void FindNearbyVenues(string latitude, string longitude)
        {
            FindNearestVenues(string.Empty, latitude, longitude);
        }
            
        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="text">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public void FindNearestVenues(string text, string latitude, string longitude)
        {
            VenueRequest vRequest = new VenueRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Text = text
            };

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(vRequest.GetUrl());

            DoRequest<VenueResponse>(req);
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public void GetCurrentUser()
        {
           GetUser(0, false, false);
        }


        /// <summary>
        /// Gets the current user.
        /// </summary>
        public void GetCurrentUser(bool badges, bool mayor)
        {
            GetUser(0, badges, mayor);
        }
      
        /// <summary>
        /// Gets a specific user.
        /// </summary>
        /// <param name="userId">User id for the user</param>
        /// <param name="twitterId">Twitter id for the user.</param>
        /// <param name="badges">Specifies whether to include badges</param>
        /// <param name="mayor">Specifies whether to include mayor tag</param>
        public void GetUser(int userId, bool badges, bool mayor)
        {
            UserRequest userRequest = new UserRequest
            {
                UserId = userId,
                Badges = badges,
                Mayor = mayor
            };

            string url = userRequest.GetUrl();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            DoRequest<UserResponse>(req);

            // TODO : raise error and add event handler here.
        }


        /// <summary>
        /// Gets the predefined categories and their sub-categories.
        /// </summary>
        public void FetchCategories()
        {
            var categoriesRequest = new CategoriesRequest();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(categoriesRequest.GetUrl());

            DoRequest<CategoriesResponse>(req);
        }


        #region Private methods

        private void DoRequest<T>(HttpWebRequest req) where T : ResponseObject
        {
            T obj = Activator.CreateInstance<T>();

            var resetEvent = new AutoResetEvent(false);
          
            req.BeginGetResponse(delegate(IAsyncResult a)
            {
                try
                {
                    string responseText = GetResponseText(req.EndGetResponse(a));
                    obj = ProcessResponse<T>(responseText);

                    if (!string.IsNullOrEmpty(obj.Error))
                    {
                        throw new FourSquareException(obj.Error);
                    }
                    RaiseEvent(obj);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                ((AutoResetEvent)a.AsyncState).Set();

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
            req.UserAgent = "Maven:0.5.0 Guid/" + Guid.NewGuid().ToString("N");
        }

        private T ProcessResponse<T>(string responseString) where T : ResponseObject
        {
            T obj;

            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            using (var reader = new StringReader(responseString))
            {
                obj = serializer.Deserialize<T>(new JsonTextReader(reader));
            }

            return obj;
        }

        private string GetResponseText(WebResponse response)
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


        private void RaiseEvent(IResponse response)
        {
            EventHandler<FourSquareEventArgs> handler = OnResponseReceived;

            if (handler != null)
            {
                handler(this, new FourSquareEventArgs(response));
            }
        }

        #endregion

        public EventHandler<FourSquareEventArgs> OnResponseReceived;

        private bool async;
        private string username;
        private string password;

        private Exception exception;
    }
}
