using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Arthus
{
    public class FourSquareContext
    {
        public FourSquareContext(string username, string password) : this(username, password, true)
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
                if (OnCheckInResponseReceived != null)
                {
                    OnCheckInResponseReceived(this, new FourSquareEventArgs<CheckInResponse>(checkInResponse.Response));
                }
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
        /// <param name="key">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public void FindNearestVenues(string key, string latitude, string longitude)
        {
            string format = "venues.json?geolat={0}&geolong={1}&l={2}{3}";

            string formattedKey = string.Empty;

            if (string.IsNullOrEmpty(key)){
                formattedKey = string.Concat("&q=", key);
            }
            
            string url = string.Format(format, latitude, longitude, 1, formattedKey);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Constants.BaseUrl + url);

            DoRequest<VenueResponse>(req, delegate(VenueResponse r)
            {
                if (OnVenueResponseReceived != null)
                {
                    OnVenueResponseReceived(this, new FourSquareEventArgs<VenueResponse>(r));
                }
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

                if (OnUserReponseReceived != null)
                {
                    OnUserReponseReceived(this, new FourSquareEventArgs<UserResponse>(userResponse));
                }
            });

            // TODO : raise error and add event handler here.
        }

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
            req.Headers.Add("Authorization", string.Format("Basic {0}", enHeader));

            req.Credentials = new NetworkCredential(username, password);
            req.UserAgent = "Arthus:1.0.0 Guid/" + Guid.NewGuid().ToString("N");
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

        public EventHandler<FourSquareEventArgs<VenueResponse>> OnVenueResponseReceived;
        public EventHandler<FourSquareEventArgs<CheckInResponse>> OnCheckInResponseReceived;
        public EventHandler<FourSquareEventArgs<UserResponse>> OnUserReponseReceived;

        private bool async;

        private string username;
        private string password;

        private Exception exception;
    }
}
