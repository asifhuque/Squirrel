namespace Squirrel.Tests
{
    public class Constants
    {
#if !SILVERLIGHT

        public static string Username = System.Configuration.ConfigurationSettings.AppSettings["username"];
        public static string Password = System.Configuration.ConfigurationSettings.AppSettings["password"];
#else
        public static string Username = "mehfuz@gmail.com";
        public static string Password = "networldFour1#";

#endif

        public const string BaseUrl = "http://api.foursquare.com/v1/";

    }
}
