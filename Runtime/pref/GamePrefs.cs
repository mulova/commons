using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace mulova.unicore
{
    [DebuggerStepThrough]
    public partial class GamePrefs
    {
        public class Group
        {
            public const string PERSISTENT = "persistent";
            public const string ACCOUNT = "account";
            public const string OPTION = "option";
        }

        public readonly string key;
        public readonly string group;

        private static List<GamePrefs> prefs;
        private static RuntimePrefs store = new RuntimePrefs();

        public GamePrefs(string name, string group = Group.ACCOUNT, object @default = null)
        {
            this.key = name;
            this.group = group;
            if (prefs == null)
            {
                prefs = new List<GamePrefs>();
            }
            prefs.Add(this);
            if (@default != null && !Exists())
            {
                if (@default is int)
                {
                    Set((int)@default);
                }
                else if (@default is bool)
                {
                    Set((bool)@default);
                }
                else if (@default is string)
                {
                    Set((string)@default);
                }
            }
        }

        public override string ToString()
        {
            return key;
        }

        public override int GetHashCode()
        {
            return key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GamePrefs)
            {
                return this.key == (obj as GamePrefs).key;
            }
            return false;
        }

        public bool Exists()
        {
            return store.Exists(key);
        }

        public int GetInt(int @default = 0)
        {
            return store.GetInt(key, @default);
        }

        public long GetLong(long @default = 0)
        {
            return store.GetLong(key, @default);
        }

        public float GetFloat(float @default = 0)
        {
            return store.GetFloat(key, @default);
        }

        public string GetString(string @default = null)
        {
            return store.GetString(key, @default);
        }

        public bool GetBool(bool @default = false)
        {
            return store.GetBool(key, @default);
        }

        public T GetEnum<T>(T @default) where T : struct, IComparable, IConvertible, IFormattable
        {
            return store.GetEnum(key, @default);
        }

        public T GetJson<T>()
        {
            return store.GetJson<T>(key);
        }

        public void GetJsonOverride<T>(T inst)
        {
            store.GetJsonOverride(key, inst);
        }

        public void Set(string @default, bool save = true)
        {
            PlayerPrefs.SetString(key, @default);
            if (save)
            {
                PlayerPrefs.Save();
            }
        }

        public void Set(int @default, bool save = true)
        {
            store.Set(key, @default, save);
        }

        public void Set(long @default, bool save = true)
        {
            store.Set(key, @default, save);
        }

        public void Set(float @default, bool save = true)
        {
            store.Set(key, @default, save);
        }

        public void Set(bool @default, bool save = true)
        {
            store.Set(key, @default, save);

        }

        public void Set(object jsonObj, bool save = true)
        {
            store.SetJson(key, jsonObj);
        }

        public void Remove(bool save = true)
        {
            PlayerPrefs.DeleteKey(key);
            if (save)
            {
                PlayerPrefs.Save();
            }
        }

        public static implicit operator int(GamePrefs g)
        {
            return g.GetInt(0);
        }

        public static implicit operator bool(GamePrefs g)
        {
            return g.GetBool(false);
        }

        public static implicit operator float(GamePrefs g)
        {
            return g.GetFloat(0);
        }

        public static void Clear(params string[] group)
        {
            HashSet<string> set = new HashSet<string>(group);
            prefs.ForEach(p => {
                if (p.group == null || set.Contains(p.group))
                {
                    p.Remove(false);
                }
            });
            PlayerPrefs.Save();
        }

        public static string Log()
        {
            var str = new System.Text.StringBuilder(10240);
            foreach (var p in prefs)
            {
                str.Append(p.key).Append('=').Append(p.GetString()).AppendLine();
            }
            return str.ToString();
        }

        public static void SetEnum<T>(string key, T val) where T : struct, IComparable, IConvertible, IFormattable
        {
            PlayerPrefs.SetString(key, val.ToString());
        }

        public static void SetBoolean(string key, bool val)
        {
            PlayerPrefs.SetInt(key, val ? 1 : 0);
        }
    

        public static void SetJson(string key, object jsonObj, bool save = true)
        {
            if (jsonObj != null)
            {
                PlayerPrefs.SetString(key, JsonUtility.ToJson(jsonObj));
            }
            else
            {
                PlayerPrefs.DeleteKey(key);
            }
            if (save)
            {
                PlayerPrefs.Save();
            }
        }

    }
}

