using NUnit.Framework;

namespace SuperNova.Tests
{
    [TestFixture]
    public class FourSquareClientTest
    {
        [Test]
        public void ShouldAssertUser()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs arg)
            {
                var userResponse = arg.Data as UserResponse;
                Assert.IsTrue(userResponse.User.Id > 0);
            };

            context.AssertUser();
        }

        [Test]
        public void ShouldAssertFindNearbyVenues()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs args)
            {
                var venue  = args.Data as VenueResponse;

                Assert.IsTrue(venue.Groups.Count > 0);
                Assert.IsTrue(venue.Groups[0].Venues[0].Id > 0);
            };

            context.FindNearbyVenues("40.7204", "-73.9933");
        }

        [Test]
        public void ShouldAssertCheckinForLatLongAndVenueName()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs args)
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

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs args)
            {
                Assert.True(args.Data.Id > 0);
            };

            context.CheckIn(new CheckInRequest{ VenueId = 9194686 });
        }
        
        [Test]
        public void ShouldAssertHierarichalCategories()
        {
            var context = new FourSquareContext(username, password, false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs args)
            {

            };

            context.FetchCategories();
        }

        private const string username = "mehfuz@gmail.com";
        private const string password = "networldF#";
    }
}
