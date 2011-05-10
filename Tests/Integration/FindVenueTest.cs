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
    //    [Test, Asynchronous]
    //    public void ShouldBeAbleToDoFindVenuesForASpecificLatLong()
    //    {
    //        var context = new FourSquareContext(async);
    //        var result = context.BeginFindVenues(23.77, 90.41);

    //        result.OnCompleted += (sender, args) =>
    //        {
    //            var data = args.Data;
    //            Assert.AreEqual(data.Groups[0].Venues[0].Latitude.ToString("0.00"), "23.77");
    //            Assert.AreEqual(data.Groups[0].Venues[0].Longitude.ToString("0.00"), "90.41");
    //            EnqueueTestComplete();
    //        };

    //        context.EndFindVenues(result);
    //    }

    //    [Test, Asynchronous]
    //    public void ShouldBeAbleToGetVenueForASpecificId()
    //    {
    //        FourSquareContext context = new FourSquareContext(async);

    //        int expected = 41710;

    //        var response = context.BeginGetVenue(expected);

    //        response.OnCompleted += (sender, args) =>
    //        {
    //            var result = args.Data;
    //            Assert.AreEqual(result.Venue.Id, expected);
    //            EnqueueTestComplete();
    //        };

    //        context.EndGetVenue(response);
    //    }
    }
}
