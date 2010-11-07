using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Squirrel.Tests
{
    public class BaseFixture 
    {
        protected const string BaseUrl = "http://api.foursquare.com/v1/";

        protected string ReadResponseFile(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            
            using (Stream stream = assembly.GetManifestResourceStream("Squirrel.Tests.Responses." + fileName))
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }


        protected FakeHttpRequestProxy GetFakeRequest(string method)
        {
            var responseString = ReadResponseFile(method + ".json");
            var fakeRequest = new FakeHttpRequestProxy(responseString);
            return fakeRequest;
        }
    }
}
