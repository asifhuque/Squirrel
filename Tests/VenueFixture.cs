using NUnit.Framework;


namespace Squirrel.Tests
{
    [TestFixture]
    public class VenueFixture : BaseFixture
    {
        [Test]
        public void ShouldAssertValidFindVenuesRequest()
        {
            string expectedUrl = string.Concat(BaseUrl, "venues.json?geolat=20.11&geolong=21.12&l=50");

            VenuesRequest request = new VenuesRequest
            {
                Limit = 50,
                Latitude = 20.11,
                Longitude = 21.12
            };

            string actualUrl = request.GetUrl();

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ShouldAssertValidFindVenueDetailRequest()
        {
            string expectedUrl = string.Concat(BaseUrl, "venue.json?vid=10");

            VenueRequest request = new VenueRequest
            {
                Id = 10
            };

            string actualUrl = request.GetUrl();

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void ShouldFailIfLimitIsMoreThatMaximum()
        {
            var request = new VenuesRequest { Limit = 100 };

#if SILVERLIGHT

            try
            {
                request.GetUrl();
                Assert.Fail();
            }
            catch(FourSquareException ex)
            {
            }

#else

            Assert.Throws<FourSquareException>(() => request.GetUrl());
#endif

        }

        [Test]
        public void ShouldAssertVenuesResponseAsExpected()
        {
            var fakeRequest = GetFakeRequest("venues");
            var context = new FourSquareContext(fakeRequest , false);

            context.FindVenues(20.10, 20.50, delegate(VenuesResponse response){
               Assert.IsTrue(response.Groups.Count == 2);
               Assert.IsTrue(response.Groups[0].Venues.Count == 2);
               Assert.IsTrue(response.Groups[1].Venues.Count == 1);

               Assert.AreEqual(response.Groups[0].Venues[0].Specials.Count, 1);
               Assert.AreEqual(response.Groups[0].Venues[0].Specials[0].Type, SpecialType.Frequency);
           
               Assert.AreEqual(response.Groups[0].Venues[1].Specials.Count, 1);
               Assert.AreEqual(response.Groups[0].Venues[1].Specials[0].Type, SpecialType.Frequency);
            });
        }


        [Test]
        public void ShouldAssertVenueResponseAsExpected()
        {
            var fakeRequest = GetFakeRequest("venue");
            var context = new FourSquareContext(fakeRequest, false);

            context.GetVenue(0, delegate(VenueResponse response){
                Assert.AreNotEqual(0, response.Venue.Id);
                Assert.AreEqual(42, response.Venue.Tips.Count);
                Assert.AreEqual(2, response.Venue.Specials.Count);
               
                Assert.IsNotNull(response.Venue.Tips[0].User);
                Assert.IsNotNull(response.Venue.Tips[0].CreatedOn);
                Assert.AreNotEqual(0, response.Venue.Tips[0].Id);
            });
        }
    }
}
