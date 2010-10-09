using System.Collections.Generic;
using Maven.Domain;
using Newtonsoft.Json;

namespace Maven
{
    public class CategoriesResponse : ResponseObject
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
