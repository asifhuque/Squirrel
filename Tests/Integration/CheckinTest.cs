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
    public class CheckinTest : BaseFixture
    {
        private const string token = "EJSAJUXKFJAWF43CB4YP1DC2EYNV3L2CSSIN2FJVKNS5CILY";

        [Test, Asynchronous, Tag("test")]
        public void ShoulAssertToExpectedResponseWhenGetAllCategoriesIsCalled()
        {
            var service = new CheckinService(CreateContext("checkin"));

            service.CheckIn(23.77, 90.41, Broadcast.Private, "Experimental").ObserveOnDispatcher().Subscribe(response =>
            {
                Assert.IsNotNull(response.CheckIn.Id);
                EnqueueTestComplete();
            });
        }

        [Test, ExpectedException(typeof(FourSquareException))]
        public void ShouldThrowWhenNeitherVenueNameOrIdSpecified()
        {
            new CheckinRequest { Longitude = 10, Latitude = 10 }.Create(CreateContext("checkin").HttpRequestProxy);
        }
    }
}
