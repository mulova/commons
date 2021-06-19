//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Reflection;

namespace mulova.unicore
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodVerifyAttribute : Attribute
    {
        public bool IsValid(object instance, MethodInfo m)
        {
            return (bool)m.Invoke(instance, null);
        }
    }
}
