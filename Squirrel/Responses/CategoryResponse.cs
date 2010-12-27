using System.Collections.Generic;
using Squirrel.Domain;
using Newtonsoft.Json;
using Squirrel.Domain.Base;

namespace Squirrel
{
    public class CategoryResponse : ResponseObject
    {
        /// <summary>
        ///  Get or sets list of categories
        /// </summary>
        [JsonProperty("categories")]
        public IList<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private IList<Category> categories = new List<Category>();

    }
}
