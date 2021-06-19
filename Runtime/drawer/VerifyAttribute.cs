//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Reflection;
using UnityEngine;

namespace mulova.unicore
{
    public abstract class VerifyAttribute : PropertyAttribute
    {
        public abstract bool IsValid(object instance, FieldInfo field);
    }
}
