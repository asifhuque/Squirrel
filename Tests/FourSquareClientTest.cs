using NUnit.Framework;
using System.Linq;

namespace Maven.Tests
{
    [TestFixture]
    public class FourSquareClientTest
    {
        [Test]
        public void ShouldAssertUserForValidCredentials()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs arg)
            {
                var userResponse = arg.Data as UserResponse;
                Assert.IsTrue(userResponse.User.Id > 0);
            };

            context.GetCurrentUser();
        }

        [Test]
        public void ShouldGetUserForUserId()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs arg)
            {
                var userResponse = arg.Data as UserResponse;

                Assert.IsTrue(userResponse.User.Id > 0);
                Assert.IsTrue(userResponse.User.Badges.Count > 0);
            };

            context.GetUser(33, true, true);
        }
        [Test]
        public void ShouldAssertResponseForFindNearByVenues()
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
                Assert.IsTrue(args.Data.Id > 0);
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
                Assert.IsTrue(args.Data.Id > 0);
            };

            context.CheckIn(new CheckInRequest{ VenueId = 9194686 });
        }
        
        [Test]
        public void ShouldAssertHierarchalCategories()
        {
            var context = new FourSquareContext(false);

            context.OnResponseReceived += delegate(object sender, FourSquareEventArgs args)
            {
                var response = args.Data as CategoriesResponse;
                var categories = response.Categories;

                var category = categories.First();

                Assert.IsNotEmpty(category.FullPathName);
                Assert.IsNotEmpty(category.NodeName);
                Assert.IsNotEmpty(category.IconUrl);

                // assert a sub-category.
                Assert.IsTrue(category.SubCategories.First().Id > 0);

            };

            context.FetchCategories();
        }

        private string username = Constants.Username;
        private string password = Constants.Password;
    }
}
