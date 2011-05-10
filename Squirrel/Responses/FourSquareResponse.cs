using System;
using Newtonsoft.Json;

namespace Squirrel.Responses
{
    public class FourSquareResponse<T> : Domain.Base.ResponseObject
    {

        /// <summary>
        /// Gets or sets target response
        /// </summary>
        [JsonProperty("response")]
        public T Response
        {
            get { return response; }
            set
            {
                response = value;
                OnPropertyChanged("Response");
            }
        }

        private T response;
    }
}
