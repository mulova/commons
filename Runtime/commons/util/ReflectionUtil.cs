//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Collections.Generic.Ex;
using System.Ex;
using System.Reflection;
using System.Text.Ex;

namespace mulova.commons
{
    public static class ReflectionUtil
	{
        public static readonly BindingFlags INSTANCE_FLAGS = BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.FlattenHierarchy;
        public static readonly BindingFlags VAR_FLAGS = INSTANCE_FLAGS&~BindingFlags.SetProperty&~BindingFlags.GetProperty;
        public static readonly ILog log = LogManager.GetLogger(nameof(ReflectionUtil));

        /// <summary>
        /// Enable reflection in iOS
        /// </summary>
        public static void ForceMonoReflection()
        {
            System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        }


		public static T NewInstance<T>()
		{
//			if (typeof(ScriptableObject).IsAssignableFrom(typeof(T)))
//			{
//				return default(T);
//			} else 
                if (typeof(string).IsAssignableFrom(typeof(T)))
			{
				return default(T);
			}
			return (T)Activator.CreateInstance(typeof(T));
			//        ConstructorInfo c = typeof(T).GetConstructor(new Type[0]);
			//        Object o = c.Invoke(new Object[] {});
			//        return (T)o;
			//		return (D)typeof(D).GetConstructor(new Type[] { typeof(E)}).Invoke(new object[] {e})
		}

		public static T NewInstance<T>(string clsName)
		{
			return (T)Activator.CreateInstance(TypeEx.GetType(clsName));
		}

		public static object Invoke(object instance, string methodName, params object[] parameters)
		{
			MethodInfo method = instance.GetType().GetMethod(methodName, ReflectionUtil.VAR_FLAGS);
			return method.Invoke(instance, parameters);
		}
    }
}
