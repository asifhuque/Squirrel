using NUnit.Framework;

namespace Squirrel.Tests.Integration
{
    [TestFixture]
    public class VenueTests
    {
        [Test]
        public void ShouldBeAbleToDoFindVenuesForASpecificLatLong()
        {
            FourSquareContext context = new FourSquareContext(false);

            context.FindVenues(23.77, 90.41, delegate(VenuesResponse response)
            {
                Assert.AreEqual(response.Groups[0].Venues[0].Latitude.ToString("0.00"), "23.77");
                Assert.AreEqual(response.Groups[0].Venues[0].Longitude.ToString("0.00"), "90.41");
            });
        }

        [Test]
        public void ShouldBeAbleToGetVenueForASpecificId()
        {
            FourSquareContext context = new FourSquareContext(false);

            int expected = 41710;

            context.GetVenue(expected, delegate(VenueResponse response)
            {
                Assert.AreEqual(response.Venue.Id, expected);
            });
        }
    }
}
