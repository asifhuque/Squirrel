using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Squirrel.Abstraction;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace Squirrel.Domain.Base
{
    public abstract class ResponseObject : INotifyPropertyChanged, IResponseObject
    {
        [JsonProperty("error")]
        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged("Error");
            }
        }

        /// <summary>
        ///  Get or sets id of the venue
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        
        /// <summary>
        /// Gets or set the meta for the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta
        {
            get { return meta; }
            set
            {
                meta = value;
                OnPropertyChanged("Meta");
            }
        }


        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("notifications")]
        public IList<Notification> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged("Notifications");
            }
        }
    
        #region INotifyPropertyChanged Members

        ///<summary>
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected virtual void OnPropertyChanged(string prop)
        {
            var e = new PropertyChangedEventArgs(prop);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        private string error;
        private string id;
        private Meta meta;
        private IList<Notification> notifications = new List<Notification>();
    }
}
