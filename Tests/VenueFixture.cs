
#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
using Squirrel.Domain;
using System;

#endif

using Squirrel.Proxy;
using Squirrel.Domain;
using Microsoft.Phone.Reactive;
using Squirrel.Abstraction;


namespace Squirrel.Tests
{
    [TestFixture]
    public class VenueFixture : BaseFixture   
    {
        [Test]
        public void ShouldAssertValidFindVenuesRequest()
        {
            string expectedUrl = string.Concat(Constants.BaseUrl, "venues/search?ll=20.11, 21.12&limit=50");

            VenueRequest request = new VenueRequest
            {
                Limit = 50,
                Latitude = 20.11,
                Longitude = 21.12
            };

            var req = request.Create(new HttpRequestProxy());

            Assert.AreEqual(expectedUrl, req.RequestUri.ToString());
        }

        [Test, Asynchronous]
        public void ShouldGetVenuesAsynchronously()
        {
            var fakeRequest = Helper.CreateFakeProxy("venues");
        
            IVenueContext context = new FourSquareContext(fakeRequest);
         
            context.Search(string.Empty, 23.77, 90.41).ObserveOnDispatcher().Subscribe(response =>
            {
                Assert.IsTrue(response.Groups.Count == 1);
                EnqueueTestComplete();
            });
        }
    }
}
