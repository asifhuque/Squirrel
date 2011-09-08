using Microsoft.Silverlight.Testing;

namespace Squirrel.Tests
{
    public partial class BaseFixture : SilverlightTest
    {
        protected FourSquareContext CreateContext(string target)
        {
            var fakeRequest = Helper.CreateFakeProxy(target);
            var context = new FourSquareContext(fakeRequest);
            
            return context;
        }
    }
}
