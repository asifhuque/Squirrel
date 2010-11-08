#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;

#endif

using System.Linq;

namespace Squirrel.Tests.Integration
{
    [TestFixture]
    public class CategoryTest : BaseFixture
    {
        [Test, Asynchronous]
        public void ShouldAssertHierarchalCategories()
        {
            var context = new FourSquareContext(async);

            context.GetCategories(delegate(CategoryResponse response)
            {
                Assert.IsTrue(response.Categories.Count > 0);
                Assert.IsTrue(response.Categories.First().SubCategories.Count > 0);

                EnqueueTestComplete();
            });
        }
    }
}
