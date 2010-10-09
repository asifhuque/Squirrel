﻿using Newtonsoft.Json;

namespace SuperNova
{
    /// <summary>
    /// Wraps the check-in response.
    /// </summary>
    internal class CheckInResponseContainer : ResponseObject
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
            }
        }

        private CheckInResponse response;
    }
}