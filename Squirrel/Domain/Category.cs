using Newtonsoft.Json;
using System.Collections.Generic;

namespace Squirrel.Domain
{
    public class Category : ResponseObject
    {
        /// <summary>
        /// Gets or sets the full path name of the category
        /// </summary>
        [JsonProperty("fullpathname")]
        public string FullPathName
        {
            get { return fullPathName; }
            set
            {
                fullPathName = value;
                OnPropertyChanged("FullPathName");
            }
        }

        /// <summary>
        /// Gets or sets the full path name of the category
        /// </summary>
        [JsonProperty("nodename")]
        public string NodeName
        {
            get { return nodeName; }
            set
            {
                nodeName = value;
                OnPropertyChanged("NodeName");
            }
        }

        /// <summary>
        /// Gets or sets the full path name of the category
        /// </summary>
        [JsonProperty("iconurl")]
        public string IconUrl
        {
            get { return iconUrl; }
            set
            {
                iconUrl = value;
                OnPropertyChanged("IconUrl");
            }
        }

        /// <summary>
        ///  Get or sets list of sub-categories
        /// </summary>
        [JsonProperty("categories")]
        public IList<Category> SubCategories
        {
            get { return subCategories; }
            set
            {
                subCategories = value;
                OnPropertyChanged("Categories");
            }
        }

        private IList<Category> subCategories = new List<Category>();

        private string fullPathName;
        private string nodeName;
        private string iconUrl;
    }
}
