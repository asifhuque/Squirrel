using System;
using System.Reflection;


namespace Squirrel
{
    internal static class Extensions
    {
        /// <summary>
        /// Tries to find the target attribute from a specific property.
        /// </summary>
        internal static T FindAttribute<T>(this ICustomAttributeProvider provider) where T : System.Attribute
        {
             object[] attributes = provider.GetCustomAttributes(typeof(T), false);

             if (attributes.Length == 1)
             {
                 return (T)attributes[0];
             }
             return default(T);
        }
    }
}
