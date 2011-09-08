
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
using Microsoft.Phone.Reactive;
using Squirrel.Abstraction;
using Squirrel.Services;
using Squirrel.Proxy;

namespace Squirrel.Tests
{
    [TestFixture]
    public class TipFixture : BaseFixture
    {
        [Test, ExpectedException(typeof(FourSquareException))]
        public void ShouldThrowExceptionWhenNoLocationIsSupplied()
        {
            var request = new Requests.Tips.SearchRequest();
            var req = request.Create(new HttpRequestProxy());
        }

        [Test, ExpectedException(typeof(FourSquareException))]
        public void ShouldThrowForInvalidVenueIdAndTextForAddRequest()
        {
            new Requests.Tips.AddRequest().Create(new HttpRequestProxy());
        }

        [Test]
        public void ShouldAssertWhenVenuIdAndTextIsProvidedForAddRequest()
        {
            new Requests.Tips.AddRequest { VenueId = "10", Text = "hello" }.Create(new HttpRequestProxy());
        }

        [Test, Asynchronous]
        public void ShouldAssertTipAdd()
        {
            var fakeRequest = Helper.CreateFakeProxy("tips");

            var context = new FourSquareContext(fakeRequest);
            var service = new TipService(context);

            service.Add("123" , "hello").ObserveOnDispatcher().Subscribe(response =>
            {
                EnqueueTestComplete();
            });

            Assert.AreEqual(fakeRequest.Request.Method, "POST");
            Assert.AreEqual(fakeRequest.Request.ContentType, "application/json");
            Assert.AreEqual("/v2/tips/add", fakeRequest.Request.RequestUri.AbsolutePath);
            Assert.AreEqual("?venueId=123&text=hello", fakeRequest.Request.RequestUri.Query);
        }

        [Test, Asynchronous]
        public void ShouldReturnTipsWhenRequestedAsynchrouslyViaObserver()
        {
            var fakeRequest = Helper.CreateFakeProxy("tips");

            var context = new FourSquareContext(fakeRequest);
            var service = new TipService(context);

            service.Search(23.77, 90.41, 0, 100).ObserveOnDispatcher().Subscribe(response =>
            {
                Assert.IsTrue(response.Tips.Count > 0);
                EnqueueTestComplete();
            });
        }
    }
}
