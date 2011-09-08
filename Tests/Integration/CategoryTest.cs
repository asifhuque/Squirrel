#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;

#endif

using System.Linq;
using Squirrel.Services;
using Microsoft.Phone.Reactive;

namespace Squirrel.Tests.Integration
{
    [TestFixture]
    public class CategoryTest : BaseFixture
    {
        [Test, Asynchronous]
        public void ShoulAssertToExpectedResponseWhenGetAllCategoriesIsCalled()
        {
            var fakeRequest = Helper.CreateFakeProxy("categories");

            var context = new FourSquareContext(fakeRequest);
            var service = new VenueService(context);

            service.GetAllCategories().ObserveOnDispatcher().Subscribe(response => 
            {
                Assert.IsTrue(response.Categories.Count == 8);
                Assert.IsTrue(response.Categories.First().Categories.Count > 0);
       
                EnqueueTestComplete();
            });
        }
    }
}
