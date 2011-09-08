using Newtonsoft.Json;
using System.Collections.Generic;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Meta : ResponseObject
    {
        /// <summary>
        ///  Gets or sets the response code for the response.
        /// </summary>
        [JsonProperty("code")]
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        private string code;
    }
}
