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
using Squirrel.Domain.Base;
using Newtonsoft.Json;

namespace Squirrel.Domain
{
    public class Contact : ResponseObject
    {
        /// <summary>
        ///  Get or sets phone number of the venue
        /// </summary>
        [JsonProperty("phone")]
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        private string phone;

    }
}
