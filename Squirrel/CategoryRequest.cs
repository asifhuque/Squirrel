using Squirrel.Abstraction;
using Squirrel.Attributes;

namespace Squirrel
{
    /// <summary>
    /// Defines the categories request.
    /// </summary>
    [RequestMethod("categories.json")]
    public class CategoryRequest : IUrlProcessor
    {
        #region IRequestUrl Members

        public string GetUrl()
        {
            return UrlProcessorFactory.Process(this);
        }

        #endregion
    }
}
