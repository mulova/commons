//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using UnityEngine;

namespace mulova.unicore
{
    public class EnumArrayAttribute : PropertyAttribute 
    {
        public Type enumType;

        public EnumArrayAttribute(Type enumType)
        {
            this.enumType = enumType;
        }
    }
}
