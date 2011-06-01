using System.Linq;

#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace Squirrel.Tests
{
    [TestFixture]
    public class FourSquareClientTest : BaseFixture
    {
        [Test, Ignore]
        public void ShouldAssertCheckinForLatLongAndVenueName()
        {
            FourSquareContext context = new FourSquareContext(username, password);

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
        
        
        [Test, Ignore]
        public void ShouldAssertCheckinForVenueId()
        {
            FourSquareContext context = new FourSquareContext(username, password);

            var request = new CheckInRequest { VenueId = 9194686, IsPrivate = true };

            context.CheckIn(request, delegate(CheckInResponse response)
            {
                Assert.IsTrue(response.Id > 0);
            });
        }
      
        private string username = Constants.Username;
        private string password = Constants.Password;
    }
}
