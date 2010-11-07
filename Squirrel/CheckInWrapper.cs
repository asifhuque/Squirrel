using Newtonsoft.Json;

namespace Squirrel
{
    /// <summary>
    /// Wraps the check-in response.
    /// </summary>
    internal class CheckInWrapper : Domain.Base.ResponseObject
    {
        [JsonProperty("checkin")]
        public CheckInResponse Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
                OnPropertyChanged("Response");
            }
        }

        private CheckInResponse response;
    }
}
