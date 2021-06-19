//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Ex;
using System.Reflection;

namespace mulova.commons
{
    public static class AttributeUtil
    {
        public static T GetAttribute<T>(object obj, string fieldName) where T : Attribute
        {
            FieldInfo f = obj.GetFieldInfo(fieldName);
            Type fieldType = f.FieldType;
            if (fieldType.IsArray)
            {
                fieldType = fieldType.GetElementType();
            }
            return fieldType.GetAttribute<T>();
        }
    }
}
