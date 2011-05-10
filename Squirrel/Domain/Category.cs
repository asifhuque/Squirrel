using Newtonsoft.Json;
using System.Collections.Generic;
using Squirrel.Domain.Base;

namespace Squirrel.Domain
{
    public class Category : ResponseObject
    {

        /// <summary>
        ///  Get or sets name of the category
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
      
        /// <summary>
        /// Gets or sets the icon for the category
        /// </summary>
        [JsonProperty("icon")]
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        /// <summary>
        /// Gets the 64x64 Icon
        /// </summary>
        public string BigIcon
        {
            get
            {
                if (!string.IsNullOrEmpty(icon))
                {
                    int ext = icon.IndexOf(".png");
                    string main = icon.Substring(0, ext);

                    main = string.Format("{0}_64.png", main);

                    return main;
                }
                return icon;
            }
        }
       

        /// <summary>
        /// Gets or sets a value indicating if the it is a 
        /// primary category.
        /// </summary>
        [JsonProperty("primary")]
        public bool Primary
        {
            get { return primary; }
            set
            {
                primary = value;
                OnPropertyChanged("Primary");
            }
        }

        /// <summary>
        ///  Get or sets list of sub-categories
        /// </summary>
        [JsonProperty("parents")]
        public IList<string> Parents
        {
            get { return parents; }
            set
            {
                parents = value;
                OnPropertyChanged("Parents");
            }
        }

        private IList<string> parents = new List<string>();

        private string icon;
        private string name;
        private bool primary;
    }
}
