namespace Maven.Tests
{
    public class Constants
    {
#if !SILVERLIGHT

        public static string Username = System.Configuration.ConfigurationSettings.AppSettings["username"];
        public static string Password = System.Configuration.ConfigurationSettings.AppSettings["password"];
#else
        public static string Username = "userName";
        public static string Password = "password";

#endif
    }
}
