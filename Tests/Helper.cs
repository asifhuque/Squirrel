using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Squirrel.Tests
{
    internal static class Helper
    {

        private static string ReadResponseFile(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream("Squirrel.Tests.Responses." + fileName))
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        internal static FakeHttpRequestProxy CreateFakeProxy(string method)
        {
            var responseString = ReadResponseFile(method + ".json");
            var fakeRequest = new FakeHttpRequestProxy(responseString);
            return fakeRequest;
        }

    }
}
