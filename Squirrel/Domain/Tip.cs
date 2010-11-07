﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirrel.Domain.Base;
using Newtonsoft.Json;

namespace Squirrel.Domain
{
    public class Tip : ResponseObject
    {
        /*
         *  "id": 14506,
                "text": "if you stay for a while the owner is more than likely to drop by with free food or drink",
                "created": "Fri, 26 Jun 09 18:45:49 +0000",
                "user": {
                    "id": 16970,
                    "firstname": "Greg",
                    "lastname": "S.",
                    "photo": "http://playfoursquare.s3.amazonaws.com/userpix_thumbs/4a4262911f7cf.jpg",
                    "gender": "male"
                }
         */

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        [JsonProperty("text")]
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        [JsonProperty("created")]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set
            {
                createdOn = value;
                OnPropertyChanged("CreatedOn");
            }
        }

        /// <summary>
        /// Gets or sets the user
        /// </summary>
        [JsonProperty("user")]
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string text;
        private DateTime createdOn;
        private User user;
    }
}
