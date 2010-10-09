
using Maven.Abstraction;
using Maven.Attributes;

namespace Maven
{
    [RequestMethod("user.json")]
    public class UserRequest : IRequestUrl
    {
        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        [RequestProperty("uid")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the twitter handle.
        /// </summary>
        /// <remarks>
        /// if you specify both user id  and twitter handle, user id will take precedence.
        /// </remarks>
        [RequestProperty("twitter")]
        public string TwitterId { get; set; }

        /// <summary>
        /// Includes the badges info for the specific user.
        /// </summary>
        [RequestProperty("badges")]
        public bool Badges { get; set; }

        /// <summary>
        /// Incldues the mayor information for the specific user.
        /// </summary>
        public bool Mayor { get; set; }

        #region IRequest Members

        public string GetUrl()
        {
            return UrlBuilder.GetUrl(this);
        }

        #endregion
    }
}
