using System;
using System.Diagnostics;
using UnityEngine;

namespace mulova.unicore
{
    [DebuggerStepThrough]
    public class PreferenceImpl : IPreference
    {
        private readonly IPreferenceImpl impl;

        public PreferenceImpl(IPreferenceImpl impl)
        {
            this.impl = impl;
        }
        public int GetInt(string key, int value = 0)
        {
            return impl.GetInt(key, value);
        }

        public long GetLong(string key, long value = 0)
        {
            if (long.TryParse(impl.GetString(key), out var l))
            {
                return l;
            }
            else
            {
                return value;
            }
        }

        public float GetFloat(string key, float value = 0)
        {
            return impl.GetFloat(key, value);
        }

        public string GetString(string key, string def = null)
        {
            return impl.GetString(key, def);
        }

        public bool GetBool(string key, bool def = false)
        {
            return impl.GetInt(key, def ? 1 : 0) != 0;
        }

        public T GetEnum<T>(string key, T val) where T : struct, IComparable, IConvertible, IFormattable
        {
            T e = val;
            var str = impl.GetString(key, null);
            if (str != null)
            {
                Enum.TryParse<T>(str, out e);
            }
            return e;
        }

        public T GetJson<T>(string key)
        {
            var json = impl.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return JsonUtility.FromJson<T>(json);
            }
            return default;
        }

        public void GetJsonOverride<T>(string key, T inst)
        {
            var json = impl.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                JsonUtility.FromJsonOverwrite(json, inst);
            }
        }

        public void Set(string key, string value, bool save = true)
        {
            impl.Set(key, value);
            if (save)
            {
                impl.Save();
            }
        }

        public void Set(string key, int value, bool save = true)
        {
            impl.Set(key, value);
            if (save)
            {
                impl.Save();
            }
        }

        public void Set(string key, long value, bool save = true)
        {
            impl.Set(key, value.ToString());
            if (save)
            {
                impl.Save();
            }
        }

        public void Set(string key, float value, bool save = true)
        {
            impl.Set(key, value);
            if (save)
            {
                impl.Save();
            }
        }

        public void Set(string key, bool value, bool save = true)
        {
            impl.Set(key, value ? 1 : 0);
            if (save)
            {
                impl.Save();
            }
        }

        public void SetEnum(string key, object value, bool save = true)
        {
            impl.Set(key, value.ToString());
            if (save)
            {
                impl.Save();
            }
        }

        public void SetJson(string key, object inst, bool save = true)
        {
            if (inst != null)
            {
                impl.Set(key, JsonUtility.ToJson(inst));
            }
            if (save)
            {
                impl.Save();
            }
        }

        public bool Exists(string key)
        {
            return impl.Exists(key);
        }

        public void Delete(string key, bool save = true)
        {
            impl.Delete(key);
            if (save)
            {
                impl.Save();
            }
        }

        public void DeleteAll()
        {
            impl.DeleteAll();
        }
    }
}

