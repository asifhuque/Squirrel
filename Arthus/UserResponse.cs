using Newtonsoft.Json;
using Arthus.Domain;
using System.Collections.Generic;

namespace Arthus
{
    internal class UserResponse
    {
        [JsonProperty("User")]
        public User User { get; set; }

        /// <summary>
        /// Gets if the login process is successful.
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
        {
            return User.Id > 0;
        }
    }
}
