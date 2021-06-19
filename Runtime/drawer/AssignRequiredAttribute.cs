//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Ex;
using System.Reflection;

namespace mulova.unicore
{
    public class AssignRequiredAttribute : VerifyAttribute
    {
        public override bool IsValid(object instance, FieldInfo field)
        {
            var val = field.GetValue(instance);
            return !val.UnityEquals(null);
        }
    }
} 
