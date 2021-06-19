using System.Reflection;

namespace System.Ex
{
    public static class FieldInfoEx
    {
        public static readonly BindingFlags INSTANCE_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
        public static readonly BindingFlags VAR_FLAGS = INSTANCE_FLAGS & ~BindingFlags.SetProperty & ~BindingFlags.GetProperty;

        public static T GetAttribute<T>(this FieldInfo f) where T : Attribute
        {
            foreach (Attribute attr in f.GetCustomAttributes(true))
            {
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                if (attr.TypeId == typeof(T))
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
                {
                    return attr as T;
                }
            }
            return null;
        }

        public static FieldInfo GetFieldInfo<T>(string fieldName)
        {
            BindingFlags flags = INSTANCE_FLAGS | BindingFlags.Static;
            return typeof(T).GetField(fieldName, flags);
        }

        public static T GetStaticFieldValue<T>(string fieldName)
        {
            FieldInfo field = GetFieldInfo<T>(fieldName);
            object v = field.GetValue(null);
            if (v == null)
            {
                return default(T);
            }
            return (T)v;
        }

        public static void SetStaticFieldValue<T>(Type type, string fieldName, T fieldValue)
        {
            BindingFlags flags = INSTANCE_FLAGS | BindingFlags.Static;
            type.GetField(fieldName, flags).SetValue(null, fieldValue);
        }
    }

}

