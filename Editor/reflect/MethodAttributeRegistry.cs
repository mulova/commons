using System;
using System.Collections.Generic.Ex;
using System.Reflection;

namespace mulova.commons
{
    public class MethodAttributeRegistry<T> where T:Attribute
    {
        private MultiMap<Type, MethodInfo> map = new MultiMap<Type, MethodInfo>(); // key: class type,  value: attribute type
        public const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        private void Bind(Type clsType)
        {
            var methods = clsType.GetMethods(FLAGS);
            map.AddKey(clsType);
            foreach (var m in methods)
            {
                if (m.GetCustomAttributes<T>() != null)
                {
                    map.Add(clsType, m);
                }
            }
        }

        public void ForEach(object instance, Action<T, MethodInfo, object> action)
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
            var methods = map[type];
            foreach (var m in methods)
            {
                var attr = m.GetCustomAttributes<T>(true);
                attr.ForEach(a =>
                {
                    action(a, m, instance);
                });
            }
        }
    }
}

