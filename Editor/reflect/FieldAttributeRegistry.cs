using System;
using System.Collections.Generic.Ex;
using System.Reflection;

namespace mulova.commons
{
    public class FieldAttributeRegistry<T> where T:Attribute
    {
        private MultiMap<Type, FieldInfo> map = new MultiMap<Type, FieldInfo>(); // key: class type,  value: attribute type
        public const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.SetField | BindingFlags.GetField;

        private void Bind(Type clsType)
        {
            var fields = clsType.GetFields(FLAGS);
            map.AddKey(clsType);
            foreach (var f in fields)
            {
                if (f.GetCustomAttribute<T>() != null)
                {
                    map.Add(clsType, f);
                }
            }
        }

        public void ForEach(object instance, Action<T, FieldInfo, object> action)
        {
            if (instance == null)
            {
                return;
            }
            var type = instance.GetType();
            if (!map.ContainsKey(type))
            {
                Bind(type);
            }
            var fields = map[type];
            foreach (var f in fields)
            {
                var attr = f.GetCustomAttributes<T>(true);
                var val = f.GetValue(instance);
                attr.ForEach(a =>
                {
                    action(a, f, val);
                });
            }
        }
    }
}

