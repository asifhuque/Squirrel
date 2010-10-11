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
        public FourSquareContext(string username, string password) : this(username, password, true)
        {
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

            DoRequest<CheckInResponseContainer>(req, delegate(CheckInResponseContainer checkInResponse)
            {
                RaiseEvent(checkInResponse.Response);
            });
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

            DoRequest<VenueResponse>(req, delegate(VenueResponse venueResponse)
            {
                RaiseEvent(venueResponse);
            });
        }

        /// <summary>
        /// Asserts the current user.
        /// </summary>
        /// <returns></returns>
        public void AssertUser()
        {
           AssertUser(0, string.Empty, false, false);
        }

        /// <summary>
        /// Asserts the current user.
        /// </summary>
        /// <param name="twitterId">Twitter id for the user.</param>
        /// <param name="badges">Specifies whether to include badges</param>
        /// <param name="mayor">Specifies whether to include mayor tag</param>
        public void AssertUser(string twitterId, bool badges, bool mayor)
        {
            AssertUser(0, twitterId, badges, mayor);
        }

        /// <summary>
        /// Asserts the current user.
        /// </summary>
        /// <param name="badges">Specifies whether to include badges</param>
        /// <param name="mayor">Specifies whether to include mayor tag</param>
        public void AssertUser(bool badges, bool mayor)
        {
            AssertUser(0, string.Empty, badges, mayor);
        }

        /// <summary>
        /// Asserts a specific user.
        /// </summary>
        /// <param name="userId">User id for the user</param>
        /// <param name="twitterId">Twitter id for the user.</param>
        /// <param name="badges">Specifies whether to include badges</param>
        /// <param name="mayor">Specifies whether to include mayor tag</param>
        public void AssertUser(int userId, string twitterId, bool badges, bool mayor)
        {
            UserRequest userRequest = new UserRequest
            {
                UserId = userId,
                TwitterId = twitterId,
                Badges = badges,
                Mayor = mayor
            };

            string url = userRequest.GetUrl();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

            // must be authorized.
            AuthorizeRequest(req, username, password);

            DoRequest<UserResponse>(req, delegate(UserResponse userResponse)
            {
                if (!userResponse.IsSuccess())
                {
                    throw new Exception("Invalid user");
                }
                RaiseEvent(userResponse);
            });

            // TODO : raise error and add event handler here.
        }


        /// <summary>
        /// Gets the predefined categories and their sub-categories.
        /// </summary>
        public void FetchCategories()
        {
            var categoriesRequest = new CategoriesRequest();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(categoriesRequest.GetUrl());

            DoRequest<CategoriesResponse>(req, delegate(CategoriesResponse categoryReponse)
            {
                RaiseEvent(categoryReponse);
            });
        }


        #region Private methods

        private void DoRequest<T>(HttpWebRequest req, Action<T> callback) where T : ResponseObject
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

                    callback(obj);
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
            if (OnResponseReceived != null)
            {
                OnResponseReceived(this, new FourSquareEventArgs(response));
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
