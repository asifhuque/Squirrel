
#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
using Squirrel.Domain;
using System;

#endif

using Squirrel.Proxy;
using Squirrel.Domain;


namespace Squirrel.Tests
{
    [TestFixture]
    public class VenueFixture : BaseFixture   
    {

        [Test]
        public void ShouldAssertValidFindVenuesRequest()
        {
            string expectedUrl = string.Concat(Constants.BaseUrl, "venues.json?geolat=20.11&geolong=21.12&l=50");

            VenuesRequest request = new VenuesRequest
            {
                Limit = 50,
                Latitude = 20.11,
                Longitude = 21.12
            };

            var req = request.Create(new HttpRequestProxy());

            Assert.AreEqual(expectedUrl, req.RequestUri.ToString());
        }

        [Test]
        public void ShouldAssertValidFindVenueDetailRequest()
        {
            string expectedUrl = string.Concat(Constants.BaseUrl, "venue.json?vid=10");

             var request = new VenueRequest
            {
                VenueId = 10
            };

            var actualUrl = request.Create(new HttpRequestProxy());

            Assert.AreEqual(expectedUrl, actualUrl.RequestUri.ToString());
        }

        [Test]
        public void ShouldFailIfLimitIsMoreThatMaximum()
        {
            var request = new VenuesRequest { Limit = 100 };

            #if SILVERLIGHT
                
            try
            {
                request.Create(new HttpRequestProxy());
                Assert.Fail();
            }
            catch(FourSquareException)
            {

            }

            #else

            Assert.Throws<FourSquareException>(() => request.Create(new HttpRequestProxy()));
            
            #endif
        }

        [Test]
        public void ShouldAssertVenuesResponseAsExpected()
        {
            var fakeRequest = Helper.CreateFakeProxy("venues");
            var context = new FourSquareContext(fakeRequest , false);

            var result = context.FindVenues(20.10, 20.50);

            Assert.IsTrue(result.Groups.Count == 2);
            Assert.IsTrue(result.Groups[0].Venues.Count == 2);
            Assert.IsTrue(result.Groups[1].Venues.Count == 1);

            Assert.AreEqual(result.Groups[0].Venues[0].Specials.Count, 1);
            Assert.AreEqual(result.Groups[0].Venues[0].Specials[0].Type, SpecialType.Frequency);

            Assert.AreEqual(result.Groups[0].Venues[1].Specials.Count, 1);
            Assert.AreEqual(result.Groups[0].Venues[1].Specials[0].Type, SpecialType.Frequency);
         }


        [Test]
        public void ShouldAssertVenueResponseAsExpected()
        {
            var fakeRequest = Helper.CreateFakeProxy("venue");
            var context = new FourSquareContext(fakeRequest, false);

            var result = context.GetVenue(0);

            Assert.AreNotEqual(0, result.Venue.Id);
            Assert.AreEqual(42, result.Venue.Tips.Count);
            Assert.AreEqual(2, result.Venue.Specials.Count);
            Assert.IsNotNull(result.Venue.Tips[0].User);
            Assert.IsNotNull(result.Venue.Tips[0].CreatedOn);
            Assert.AreNotEqual(0, result.Venue.Tips[0].Id);
        }

        [Test]
        public void ShouldReturnDefaultCategoryForNone()
        {
            var fakeRequest = Helper.CreateFakeProxy("venues");
            var context = new FourSquareContext(fakeRequest, false);

            string expected = "http://foursquare.com/img/categories/none.png";

            var result = context.FindVenues(0.0, 0.0);

            var category = result.Groups[0].Venues[0].Category;

            Assert.IsNotNull(category);
            Assert.AreEqual(expected, category.IconUrl);
        }
    }
}
