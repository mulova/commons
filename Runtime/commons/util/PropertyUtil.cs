//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Reflection;

namespace mulova.commons
{
    public static class PropertyUtil
    {
        public static PropertyInfo GetProperty(object obj, string propName)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty | BindingFlags.SetProperty;
            return obj.GetType().GetProperty(propName, flags);
        }

        public static object GetPropertyValue(object obj, string propName)
        {
            return GetProperty(obj, propName).GetValue(obj, null);
        }

        public static void SetPropertyValue(object obj, string propName, object val)
        {
            GetProperty(obj, propName).SetValue(obj, val, null);
        }
    }
}
