using System;
using System.Reflection;
using Maven.Attributes;

namespace Maven
{
    internal class UrlBuilder
    {
        /// <summary>
        /// Gets the REST url constructed from a specific request object.
        /// </summary>
        /// <param name="args">Request object</param>
        /// <returns>Complete request url.</returns>
        public static string GetUrl(object target)
        {
            Type targetType = target.GetType();
            string url = GetUrlFromType(targetType);

            url = string.Concat(url, "?");

            foreach (PropertyInfo propertyInfo in targetType.GetProperties())
            {
                object value = propertyInfo.GetValue(target, null);

                if (value != null && !value.Equals(GetDefautlValue(propertyInfo.PropertyType)))
                {
                    object[] attributes = propertyInfo.GetCustomAttributes(typeof(RequestPropertyAttribute), false);

                    if (attributes.Length == 1)
                    {
                        RequestPropertyAttribute requestAttribute = attributes[0] as RequestPropertyAttribute;

                        // convert boolean to int.
                        if (propertyInfo.PropertyType == typeof(bool))
                        {
                            value = Convert.ToInt32(value);
                        }

                        url += string.Concat(requestAttribute.ElementName, "=",  value.ToString().Replace(' ' , '+'));
                        url += '&';
                    }
                }

            }

            if (url.IndexOf('&') >= 0 || url[url.Length -1] == '?')
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
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
                url = Constants.BaseUrl + requestMethodAttr.Method;
            }
            return url;
        }
    }
}
