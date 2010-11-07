﻿using Newtonsoft.Json;
using Squirrel.Domain;

namespace Squirrel
{
    public class UserResponse : Domain.Base.ResponseObject
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [JsonProperty("User")]
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// Gets if the login process is successful.
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
        {
            return User.Id > 0;
        }

        private User user;
    }
}
