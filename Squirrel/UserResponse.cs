using Newtonsoft.Json;
using Squirrel.Domain;

namespace Squirrel
{
    public class UserResponse : ResponseObject
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
