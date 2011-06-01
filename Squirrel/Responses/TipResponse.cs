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
using System.Collections.Generic;
using Squirrel.Domain;

namespace Squirrel
{
    public class TipResponse : Domain.Base.ResponseObject
    {
        /// <summary>
        ///  Get or sets id of the user
        /// </summary>
        [JsonProperty("tips")]
        public IList<Tip> Tips
        {
            get { return tips; }
            set
            {
                tips = value;
                OnPropertyChanged("Tips");
            }
        }

        private IList<Tip> tips = new List<Tip>();

    }
}
