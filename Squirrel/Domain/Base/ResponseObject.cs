using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Squirrel.Abstraction;
using System.ComponentModel;

namespace Squirrel.Domain.Base
{
    public class ResponseObject : INotifyPropertyChanged, IDomainResponse
    {
        /// <summary>
        ///  Get or sets id of the venue
        /// </summary>
        [JsonProperty("id")]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

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

        private int id;
        private string error;
    }
}
