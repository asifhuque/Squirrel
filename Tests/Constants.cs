namespace Squirrel.Tests
{
    public class Constants
    {
#if !SILVERLIGHT

        public static string Username = System.Configuration.ConfigurationSettings.AppSettings["username"];
        public static string Password = System.Configuration.ConfigurationSettings.AppSettings["password"];
#else
        public static string Username = "username";
        public static string Password = "password";

#endif

        public const string BaseUrl = "http://api.foursquare.com/v1/";

    }
}
