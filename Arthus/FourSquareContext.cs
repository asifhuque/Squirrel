using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Arthus.Domain;
using System.Collections.Generic;

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
                RaiseOnDataReceived(checkInResponse.Response);
            });
        }

        /// <summary>
        /// Searches a group of venues for a specific location and keyword
        /// </summary>
        /// <param name="key">Matching venue to search for (use null/empty to search all)</param>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public void FindNearbyVenues(string key, string latitude, string longitude, Action<IList<VenueGroup>> sucessCallback)
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
                if (sucessCallback != null)
                {
                    sucessCallback(r.Groups);
                }
            });
        }

        /// <summary>
        /// Validates the credential and returns user info from foursquare.
        /// </summary>
        /// <returns></returns>
        public void ValidateUser(Action<User> successCallback)
        {
            GetUser(0, string.Empty, false, false, successCallback);
        }

        public void GetUser(int userId, string twitterId, bool badges, bool mayor, Action<User> callback)
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

                if (callback != null)
                {
                    callback(userResponse.User);
                }
            });

            // TODO : raise error and add event handler here.
        }

        private void RaiseOnDataReceived<T>(T data)
        {
            if (OnDataReceived != null)
            {
                OnDataReceived(this, new FourSquareEventArgs(data));
            }
        }

        private void DoRequest<T>(HttpWebRequest req, Action<T> callback)
        {
            T obj = Activator.CreateInstance<T>();

            var resetEvent = new AutoResetEvent(false);

            req.BeginGetResponse(delegate(IAsyncResult a)
            {
                try
                {
                    string responseText = GetResponseText(req.EndGetResponse(a));
                    obj = ProcessResponse<T>(responseText);

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
                throw new Exception(exception.Message);
            }
        }

        private void AuthorizeRequest(HttpWebRequest req, string username, string password)
        {
            string enHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
            req.Headers.Add("Authorization", string.Format("Basic {0}", enHeader));

            req.Credentials = new NetworkCredential(username, password);
            req.UserAgent = "Arthus:1.0.0 Guid/" + Guid.NewGuid().ToString("N");
        }

        private T ProcessResponse<T>(string responseString)
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

        public EventHandler<FourSquareEventArgs> OnDataReceived;

        private bool async;

        private string username;
        private string password;

        private Exception exception;
    }
}
