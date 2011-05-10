using Squirrel.Abstraction;
using Squirrel.Attributes;
using System.Net;

namespace Squirrel
{
    /// <summary>
    /// Defines the categories request.
    /// </summary>
    [RequestMethod("categories.json")]
    public class CategoryRequest : Request
    {
        #region IRequestUrl Members

        public override HttpWebRequest Create(IHttpRequestProxy proxy)
        {
            return Create(this, proxy);
        }

        #endregion
    }
}
