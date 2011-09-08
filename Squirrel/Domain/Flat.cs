using Newtonsoft.Json;
using System.Collections.Generic;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Flat : ResponseObject
    {
        [JsonProperty("message")]
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Message");
            }
        } 
   
        private string text;
  
    }
}
