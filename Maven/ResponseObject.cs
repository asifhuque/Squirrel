using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using Maven.Abstraction;

namespace Maven
{
    public class ResponseObject : INotifyPropertyChanged, IResponse
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

        #region INotifyPropertyChanged Members

        ///<summary>
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

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
