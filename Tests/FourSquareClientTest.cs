using NUnit.Framework;
using System.Linq;

namespace Squirrel.Tests
{
    [TestFixture]
    public class FourSquareClientTest
    {
        [Test]
        public void ShouldAssertUserForValidCredentials()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            context.GetCurrentUser(delegate(UserResponse response)
            {
                Assert.IsTrue(response.User.Id > 0);
            });
        }

        [Test]
        public void ShouldGetUserForUserId()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            int userId = 33;

            bool badges = true;
            bool mayor = true;

            context.GetUser(userId, badges, mayor, delegate(UserResponse response)
            {
                Assert.IsTrue(response.User.Id > 0);
                Assert.IsTrue(response.User.Badges.Count > 0);
            });
        }

        [Test]
        public void ShouldAssertCheckinForLatLongAndVenueName()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);
        
            var request = new CheckInRequest
            {
                Venue = "Barista",
                Latitude = "23.776516914367676",
                Longitude = "90.41707277297974",
                IsPrivate = true,
            };

            context.CheckIn(request, delegate(CheckInResponse response)
            {
                Assert.IsTrue(response.Id > 0);
            });
        }

        [Test]
        public void ShouldAssertCheckinForVenueId()
        {
            FourSquareContext context = new FourSquareContext(username, password, false);

            var request = new CheckInRequest { VenueId = 9194686, IsPrivate = true };

            context.CheckIn(request, delegate(CheckInResponse response)
            {
                Assert.IsTrue(response.Id > 0);
            });
        }
        
        [Test]
        public void ShouldAssertHierarchalCategories()
        {
            var context = new FourSquareContext(false);

            context.GetCategories(delegate(CategoryResponse response)
            {
                var categories = response.Categories;

                var category = categories.First();

                Assert.IsNotEmpty(category.FullPathName);
                Assert.IsNotEmpty(category.NodeName);
                Assert.IsNotEmpty(category.IconUrl);

                Assert.IsTrue(category.SubCategories.First().Id > 0);
            });
        }

        private string username = Constants.Username;
        private string password = Constants.Password;
    }
}
