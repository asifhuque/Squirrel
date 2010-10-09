using Maven.Abstraction;
using Maven.Attributes;

namespace Maven
{
    /// <summary>
    /// Defines the categories request.
    /// </summary>
    [RequestMethod("categories.json")]
    public class CategoriesRequest : IRequestUrl
    {
        #region IRequestUrl Members

        public string GetUrl()
        {
            return UrlBuilder.GetUrl(this);
        }

        #endregion
    }
}
