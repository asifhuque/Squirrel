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
    public class AuthorizationTest : BaseFixture
    {
        static AuthorizationTest()
        {
            Client.Key = "XEDFVOS2ZRQTCXUCOVP2INVQIZYQMYAVQ4F1OF2FN33NHG2U";
            Client.Secret = "D21NTBFYU3YSTWAULSD2PJHMFXQAXXOQ4QXZ03BMWAQAGQN5";
        }

        [Test, Asynchronous]
        public void ShouldAuthenticateUserForValidCredentials()
        {

        }

    //    [Test, Asynchronous]
    //    public void ShouldAssertUserForValidCredentials()
    //    {
    //        var context = new FourSquareContext(username, password, async);

    //        var result = context.BeginGetUser();

    //        result.OnCompleted += (sender, args) =>
    //        {
    //            var data = args.Data;
    //            Assert.IsTrue(data.User.Id > 0);
    //            EnqueueTestComplete();
    //        };


    //        context.EndGetUser(result);
    //    }

    //    [Test, Asynchronous]
    //    public void ShouldAssertGetUserForId()
    //    {
    //        FourSquareContext context = new FourSquareContext(username, password, async);

    //        int userId = 33;

    //        bool badges = false;
    //        bool mayor = false;

    //        var result = context.BeginGetUser(userId, true);

            
    //        result.OnCompleted += (sender, args) =>
    //        {
    //            var data = args.Data;
    //            Assert.AreEqual(data.User.Id, 33);
    //            EnqueueTestComplete();
    //        };

    //        context.EndGetUser(result);
    //    }

        private string username = Constants.Username;
        private string password = Constants.Password;

    }
}
