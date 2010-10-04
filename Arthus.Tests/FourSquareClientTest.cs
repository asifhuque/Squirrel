using System.Collections.Generic;
using NUnit.Framework;
using Arthus.Domain;

namespace Arthus.Tests
{
    [TestFixture]
    public class FourSquareClientTest
    {
        [Test]
        public void ShouldAssertUser()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnUserReponseReceived += delegate(object sender, FourSquareEventArgs<UserResponse> args)
            {
                Assert.IsTrue(args.Data.User.Id > 0);
            };

            context.AssertUser();
        }

        [Test]
        public void ShouldAssertFindNearbyVenues()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnVenueResponseReceived += delegate(object sender, FourSquareEventArgs<VenueResponse> args)
            {
                IList<VenueGroup> groups  = args.Data.Groups;

                Assert.IsTrue(groups.Count > 0);
                Assert.IsTrue(groups[0].Venues[0].Id > 0);
            };

            context.FindNearbyVenues("40.7204", "-73.9933");
        }

        [Test]
        public void ShouldAssertCheckinForLatLongAndVenueName()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnCheckInResponseReceived += delegate(object sender, FourSquareEventArgs<CheckInResponse> args)
            {
                Assert.True(args.Data.Id > 0);
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

            context.OnCheckInResponseReceived += delegate(object sender, FourSquareEventArgs<CheckInResponse> checkIn)
            {
                Assert.True(checkIn.Data.Id > 0);
            };

            context.CheckIn(new CheckInRequest{ VenueId = 9194686 });
        }

        private const string username = "mehfuz@gmail.com";
        private const string password = "networldF#";
    }
}
