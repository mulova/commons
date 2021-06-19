//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using UnityEngine;

namespace mulova.unicore
{
    public class EnumPopupAttribute : PropertyAttribute
    {
        public readonly string enumTypeVar;
        
        public EnumPopupAttribute(string enumVar)
        {
            this.enumTypeVar = enumVar;
        }
    }
}
