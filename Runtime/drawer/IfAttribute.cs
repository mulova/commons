//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Reflection;
using System.Ex;

namespace mulova.unicore
{
    public class IfAttribute : VerifyAttribute
    {
        public readonly string refPropertyPath;
        public readonly object value;
        public readonly IfAction action;

        public IfAttribute(string propPath, object val, IfAction action)
        {
            this.refPropertyPath = propPath;
            this.value = val;
            this.action = action;
        }

        public IfAttribute(object val, IfAction action)
        {
            this.refPropertyPath = null;
            this.value = val;
            this.action = action;
        }

        public override bool IsValid(object instance, FieldInfo field)
        {
            if (action != IfAction.Error)
            {
                return true;
            }
            var val = field.GetValue(instance);
            return !val.UnityEquals(value);
        }
    }
}
