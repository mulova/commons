using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic.Ex;
using System.Ex;
using System.Reflection;

namespace mulova.commons
{
    /**
	 * Search the member variable's value
	 * Exclude() to exclude specific type's properties/fields
	 */
    public class MemberInfoRegistry : Loggable
    {
        public delegate bool Filter(MemberInfo m);
        public delegate bool IsFound(object fieldValue, object refValue);

        private MultiMap<Type, FieldInfo> fieldMap = new MultiMap<Type, FieldInfo>();
        private MultiMap<Type, PropertyInfo> propMap = new MultiMap<Type, PropertyInfo>();
        private HashSet<Type> excludeTypes = new HashSet<Type>();
        private MultiKeyMap<string, string, bool> excludeMembers = new MultiKeyMap<string, string, bool>();
        public virtual Filter filter { get; protected set; } = _ => true;
        public IsFound isFound = (fv, rv) => fv == rv;
        public BindingFlags fieldFlags = FIELD_FLAGS;
        public BindingFlags propertyFlags = PROPERTY_FLAGS;
        private HashSet<int> fieldTraveled = new HashSet<int>();
        private HashSet<int> propertyTraveled = new HashSet<int>();
		
        public static BindingFlags FIELD_FLAGS = BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.FlattenHierarchy|BindingFlags.SetField|BindingFlags.GetField;
        public static BindingFlags PROPERTY_FLAGS = BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.FlattenHierarchy|BindingFlags.SetProperty|BindingFlags.GetProperty;

        public void ExcludeType(params Type[] types)
        {
            if (fieldMap.Count > 0)
            {
                Assert.Fail(null, "Already binded");
            }
            foreach (Type t in types)
            {
                excludeTypes.Add(t);
            }
        }

        public void ExcludeField(string typeName, string fieldName)
        {
            if (fieldMap.Count > 0)
            {
                Assert.Fail(null, "Already binded");
            }
            excludeMembers.Add(typeName, fieldName, true);
        }

        public List<FieldInfo> GetFields(Type type)
        {
            if (!fieldMap.ContainsKey(type))
            {
                Bind(type);
            }
            return fieldMap[type];
        }

        public List<PropertyInfo> GetProperties(Type type)
        {
            if (!propMap.ContainsKey(type))
            {
                Bind(type);
            }
            return propMap[type];
        }

        public void BeginSearch()
        {
            fieldTraveled.Clear();
            propertyTraveled.Clear();
        }

        private void Bind(Type clsType)
        {
            foreach (FieldInfo f in clsType.GetFields(fieldFlags))
            {
                if (excludeTypes.Contains(f.DeclaringType) || excludeMembers.Get(f.DeclaringType.FullName, f.Name))
                {
                    continue;
                }
                // skip deprecated
                bool exclude = false;
                foreach (object a in f.GetCustomAttributes(true))
                {
                    if (a is ObsoleteAttribute)
                    {
                        exclude = true;
                        break;
                    }
                }
                if (exclude)
                {
                    continue;
                }
                if (filter(f)&&!f.IsLiteral)
                {
                    fieldMap.Add(clsType, f);
                }
            }

            foreach (PropertyInfo p in clsType.GetProperties(propertyFlags))
            {
                if (!p.CanRead)
                {
                    continue;
                }
                if (excludeTypes.Contains(p.DeclaringType) || excludeMembers.Get(p.DeclaringType.FullName, p.Name))
                {
                    continue;
                }

                //// Renderer.material in EditorMode is skipped because of memory leak
                //if (!Application.isPlaying&&p.Name == "material" && p.DeclaringType.IsAssignableFrom(typeof(Renderer)))
                //{
                //    continue;
                //}
                //if (!Application.isPlaying&&p.Name == "mesh" && p.DeclaringType.IsAssignableFrom(typeof(MeshFilter)))
                //{
                //    continue;
                //}

                // skip deprecated
                bool exclude = false;
                foreach (object a in p.GetCustomAttributes(true))
                {
                    if (a is ObsoleteAttribute)
                    {
                        exclude = true;
                        break;
                    }
                }
                if (exclude)
                {
                    continue;
                }
                if (filter(p))
                {
                    propMap.Add(clsType, p);
                }
            }
        }

        /**
		 * @return changed members/properties
		 */
        public bool HasValue(object obj, object val)
        {
            return GetFieldForValue(obj, val) != null||GetPropertyForValue(obj, val) != null;
        }

        /// <summary>
        /// Gets the first FieldInfo containing val.
        /// For array, only 1D array is supported.
        /// </summary>
        /// <returns>The field for value.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="val">Value.</param>
        public FieldInfo GetFieldForValue(object obj, object val)
        {
            if (val == null)
            {
                return null;
            }
            int hash = obj.GetHashCode();
            if (fieldTraveled.Contains(hash))
            {
                return null;
            }
            fieldTraveled.Add(hash);

            IEnumerable<FieldInfo> fieldMatch = GetFields(obj.GetType());
            if (fieldMatch != null)
            {
                foreach (FieldInfo f in fieldMatch)
                {
                    object fval = null;
					try
					{
						fval = f.GetValue(obj);
					} catch (Exception ex) {
                        log.Error(ex);
					}
                    if (isFound(fval, val))
                    {
                        return f;
                    } else if (typeof(IEnumerable).IsAssignableFrom(f.FieldType))
                    {
                        IEnumerable collection = fval as IEnumerable;
                        if (!EqualityComparer<IEnumerable>.Default.Equals(collection, default(IEnumerable)))
                        {
                            foreach (object e in collection)
                            {
                                if (isFound(e, val))
                                {
                                    return f;
                                } else if (e != null) {
                                    FieldInfo recurse = GetFieldForValue(e, val);
                                    if (recurse != null)
                                    {
                                        return recurse;
                                    }
                                }
                            }
                        }
                    } else if (fval != null && fval.GetType().GetAttribute<SerializableAttribute>() != null)
                    {
                        return GetFieldForValue(fval, val);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the first PropertyInfo containing val.
        /// For array, only 1D array is supported.
        /// </summary>
        /// <returns>The property field for value.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="val">Value.</param>
        public PropertyInfo GetPropertyForValue(object obj, object val)
        {
            if (val == null)
            {
                return null;
            }
            int hash = obj.GetHashCode();
            if (propertyTraveled.Contains(hash))
            {
                return null;
            }
            propertyTraveled.Add(hash);

            IEnumerable<PropertyInfo> propMatch = GetProperties(obj.GetType());
            if (propMatch != null)
            {
                foreach (PropertyInfo p in propMatch)
                {
                    //// Renderer.material in EditorMode is skipped because of memory leak
                    //if (!Application.isPlaying&&p.Name == "material"&&p.DeclaringType.IsAssignableFrom(typeof(Renderer)))
                    //{
                    //    continue;
                    //}
                    //if (!Application.isPlaying&&p.Name == "mesh"&&p.DeclaringType.IsAssignableFrom(typeof(MeshFilter)))
                    //{
                    //    continue;
                    //}
                    MethodInfo set = p.GetSetMethod(true);
                    MethodInfo get = p.GetGetMethod(true);
                    if (get != null && set != null)
                    {
                        ParameterInfo[] param = get.GetParameters();
                        if (param.IsEmpty())
                        {
							object oval = null;
#pragma warning disable ERP022 // Unobserved exception in generic exception handler
							try
							{
								oval = p.GetValue(obj, null);
							} catch {
                            }
#pragma warning restore ERP022 // Unobserved exception in generic exception handler
                            if (oval == null)
                            {
                                return null;
                            }
                            if (oval == val)
                            {
                                return p;
                            } else if (!(oval is string) && !oval.GetType().IsPrimitive && oval.GetType().GetAttribute<SerializableAttribute>() != null)
                            {
                                return GetPropertyForValue(oval, val);
                            }
                        }
                    }
                }
            }
            return null;
        }


        public static bool RefFilter(MemberInfo m)
        {
            if (m is FieldInfo)
            {
                FieldInfo f = m as FieldInfo;
                if (f.FieldType == typeof(String))
                {
                    return false;
                }
                return f.FieldType.IsClass;
            } else if (m is PropertyInfo)
            {
                PropertyInfo p = m as PropertyInfo;
                if (p.PropertyType == typeof(String))
                {
                    return false;
                }
                return p.PropertyType.IsClass;
            }
            return false;
        }
    }
	
}

