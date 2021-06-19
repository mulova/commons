using System;

namespace mulova.commons
{
    public class ClassAttributeRegistry
    {
        private MultiMap<Type, Type> map = new MultiMap<Type, Type>(); // key: class type,  value: attribute type

        private void Bind(Type clsType)
        {
            object[] attrs = clsType.GetCustomAttributes(true);
            map.AddKey(clsType);
            foreach (var o in attrs)
            {
                map.Add(clsType, o.GetType());
            }
        }

        public bool Contains(Type clsType, Type attrType)
        {
            if (!map.ContainsKey(clsType))
            {
                Bind(clsType);
            }
            return map.Contains(clsType, attrType);
        }
    }
}

