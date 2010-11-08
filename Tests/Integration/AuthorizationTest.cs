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
        [Test, Asynchronous]
        public void ShouldAssertUserForValidCredentials()
        {
            FourSquareContext context = new FourSquareContext(username, password, async);

            context.GetCurrentUser(delegate(UserResponse response){
                Assert.IsTrue(response.User.Id > 0);

                EnqueueTestComplete();
            });
        }

        [Test, Asynchronous]
        public void ShouldAssertGetUserForId()
        {
            FourSquareContext context = new FourSquareContext(username, password, async);

            int userId = 33;

            bool badges = false;
            bool mayor = false;

            context.GetUser(userId, badges, mayor, delegate(UserResponse response)
            {
                Assert.AreEqual(response.User.Id, 33);

                EnqueueTestComplete();
            });
        }

        private string username = Constants.Username;
        private string password = Constants.Password;

    }
}
