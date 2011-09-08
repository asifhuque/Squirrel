using System;
using System.Reflection;
using Squirrel.Attributes;
using System.Collections.Generic;
using System.Text;

namespace Squirrel
{
    internal class EndpointProcessor
    {
        internal EndpointProcessor(object target)
        {
            this.target = target;
        }

        public string GetBaseUrl()
        {
            Type targetType = target.GetType();
            return GetUrlFromType(targetType);
        }

        public IList<NameValuePair> GetParameters()
        {
            Type targetType = target.GetType();
            IList<NameValuePair> list = new List<NameValuePair>();

            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var propertyInfo in targetType.GetProperties(flags))
            {
                object value = propertyInfo.GetValue(target, null);

                if (value != null && !value.Equals(GetDefautlValue(propertyInfo.PropertyType)))
                {
                    object[] attributes = propertyInfo.GetCustomAttributes(typeof(RequestPropertyAttribute), true);

                    if (attributes.Length == 1)
                    {
                        var requestAttribute = attributes[0] as RequestPropertyAttribute;

                        // convert boolean to int.
                        if (propertyInfo.PropertyType == typeof(bool))
                            value = Convert.ToInt32(value);

                        if (requestAttribute.Encode)
                            value = Encode(value.ToString());

                        list.Add(new NameValuePair { Name = requestAttribute.ElementName, Value = value.ToString() });
                    }
                }
                else
                {
                    var requried = propertyInfo.FindAttribute<RequiredAttribute>();

                    if (requried != null)
                    {
                        throw new FourSquareException(requried.Message);
                    }
                }

            }

            return list;
        }

        static string Encode(string value)
        {
            var result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets the default value of the type.
        /// </summary>
        /// <param name="targetType">Specific target type</param>
        /// <returns>Default value</returns>
        private static object GetDefautlValue(Type targetType)
        {
            if (targetType.IsValueType || targetType.IsEnum)
            {
                return Activator.CreateInstance(targetType);
            }
            else if (targetType == typeof(string))
            {
                return string.Empty;
            }

            return null;
        }

        private static string GetUrlFromType(Type targetType)
        {
            string url = string.Empty;

            object[] methodAttribes = targetType.GetCustomAttributes(typeof(RequestMethodAttribute), false);

            if (methodAttribes.Length == 1)
            {
                var requestMethodAttr = methodAttribes[0] as RequestMethodAttribute;

                if (requestMethodAttr.Method.IndexOf("http://") >= 0)
                {
                    url = requestMethodAttr.Method;
                }
                else
                {
                    url = GetEndPointAddress(targetType) + requestMethodAttr.Method;
                }
            }
            return url;
        }

        static string GetEndPointAddress(Type target)
        {
           var attribute =  target.FindAttribute<VersionAttribute>();

            if (attribute != null && !attribute.IsLegacy)
            {
                return Constants.EndPoint_V2;
            }
            return Constants.EndPoint_V1;
        }

        private object target;
        private const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
    }
}
