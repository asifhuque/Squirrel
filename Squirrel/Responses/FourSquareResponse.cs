using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
