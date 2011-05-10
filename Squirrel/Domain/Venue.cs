using Newtonsoft.Json;
using System.Collections.Generic;
using Squirrel.Domain.Base;
using System.Linq;

namespace Squirrel.Domain
{
    public class Venue : ResponseObject
    {
        /// <summary>
        ///  Get or sets id of the venue
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
        ///  Get or sets address of the venue
        /// </summary>
        [JsonProperty("location")]
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        /// <summary>
        /// Gets the primary category associated with the venue. If
        /// nothing is specified then it will return none.png.
        /// </summary>
        public Category PrimaryCategory
        {
            get
            {
                if (primaryCategory == null)
                {
                    primaryCategory = categories.Where(x => x.Primary == true).FirstOrDefault();

                    if (primaryCategory == null)
                    {
                        primaryCategory = new Category { Icon = "http://foursquare.com/img/categories/none.png" };
                    }
                }

                return primaryCategory;
            }
            set
            {
                // useful for setting design data.
                primaryCategory = value;
            }
        }

        /// <summary>
        ///  Get or sets contact of the venue
        /// </summary>
        [JsonProperty("contact")]
        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnPropertyChanged("Contact");
            }
        }


        /// <summary>
        ///  Get or sets if the venue is verified.
        /// </summary>
        [JsonProperty("verified")]
        public bool Verfied
        {
            get { return verified; }
            set
            {
                verified = value;
                OnPropertyChanged("Verified");
            }
        }

        /// <summary>
        ///  Get or sets phone number of the venue
        /// </summary>
        [JsonProperty("stats")]
        public Stats Stats
        {
            get { return stats; }
            set
            {
                stats = value;
                OnPropertyChanged("Stats");
            }
        }

        /// <summary>
        ///  Get or sets the tips for the venue.
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

        /// <summary>
        ///  Get or set the specials associated with this venue.
        /// </summary>
        [JsonProperty("specials")]
        public IList<Special> Specials
        {
            get { return specials; }
            set
            {
                specials = value;
                OnPropertyChanged("Specials");
            }
        }

        /// <summary>
        ///  Get or sets the categories associated with the venue.
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
    

        /// <summary>
        ///  Get or sets the twitter id of the venue
        /// </summary>
        [JsonProperty("twitter")]
        public string Twitter
        {
            get { return twitter; }
            set
            {
                twitter = value;
                OnPropertyChanged("Twitter");
            }
        }

        private string name;
        private bool verified;
        private Stats stats;
        private string twitter;
        private Contact contact;

        private IList<Special> specials = new List<Special>();
        private IList<Tip> tips = new List<Tip>();
        private Location location = new Location();
        
        private IList<Category> categories = new List<Category>();
        private Category primaryCategory;

    }
}
