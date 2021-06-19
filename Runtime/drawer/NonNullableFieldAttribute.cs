//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Reflection;
using System.Ex;

namespace mulova.unicore
{
    public class NonNullableFieldAttribute : VerifyAttribute
    {
        public override bool IsValid(object instance, FieldInfo field)
        {
            return !field.GetValue(instance).UnityEquals(null);
        }
    }
}
