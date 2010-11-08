#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;

#endif

namespace Squirrel.Tests.Integration
{
    [TestFixture]
    public class FindVenueTest : BaseFixture
    {
        [Test, Asynchronous]
        public void ShouldBeAbleToDoFindVenuesForASpecificLatLong()
        {
            FourSquareContext context = new FourSquareContext(async);

            context.FindVenues(23.77, 90.41, delegate(VenuesResponse response)
            {
                Assert.AreEqual(response.Groups[0].Venues[0].Latitude.ToString("0.00"), "23.77");
                Assert.AreEqual(response.Groups[0].Venues[0].Longitude.ToString("0.00"), "90.41");

                EnqueueTestComplete();
            });
        }

        [Test, Asynchronous]
        public void ShouldBeAbleToGetVenueForASpecificId()
        {
            FourSquareContext context = new FourSquareContext(async);

            int expected = 41710;

            context.GetVenue(expected, delegate(VenueResponse response)
            {
                Assert.AreEqual(response.Venue.Id, expected);

                EnqueueTestComplete();
            });
        }
    }
}
