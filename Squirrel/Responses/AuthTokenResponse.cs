using System;
using Newtonsoft.Json;

namespace Squirrel.Responses
{
    public class AuthTokenResponse : Domain.Base.ResponseObject
    {
        /// <summary>
        /// Gets or sets the access token
        /// </summary>
        [JsonProperty("access_token")]
        public string Token
        {
            get 
            { 
                return token; 
            }
            set
            {
                token = value;
            }
        }

        private string token;
    }
}
