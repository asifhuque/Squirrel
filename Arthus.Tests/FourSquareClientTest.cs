using System.Collections.Generic;
using NUnit.Framework;
using Arthus.Domain;

namespace Arthus.Tests
{
    [TestFixture]
    public class FourSquareClientTest
    {
        [Test]
        public void ShouldAssertUserLogin()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.ValidateUser(delegate(User user)
            {
                Assert.IsTrue(user.Id > 0);
            });
        }

        [Test]
        public void ShouldAssertFindNearbyVenues()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.FindNearbyVenues(null, "40.7204", "-73.9933", delegate(IList<VenueGroup> groups)
            {
                Assert.IsTrue(groups.Count > 0);
                Assert.IsTrue(groups[0].Venues[0].Id > 0);
            });
        }

        [Test]
        public void ShouldAssertCheckinForLatLongAndVenueName()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnDataReceived += delegate(object sender, FourSquareEventArgs args)
            {
                var checkIn = args.Data as CheckInResponse;
                Assert.True(checkIn.Id > 0);
            };

            context.CheckIn(new CheckInRequest{
                Venue = "Barista",
                Latitude = "23.776516914367676",
                Longitude = "90.41707277297974"
            });
        }

        [Test]
        public void ShouldAssertCheckinForVenueId()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnDataReceived += delegate(object sender, FourSquareEventArgs args)
            {
                var checkIn = args.Data as CheckInResponse;
                Assert.True(checkIn.Id > 0);
            };

            context.CheckIn(new CheckInRequest{VenueId = 9194686 });
        }

        private const string username = "mehfuz@gmail.com";
        private const string password = "networldF#";
    }
}
