using System;

namespace mulova.unicore
{
    public interface IPreference
    {
        int GetInt(string key, int @default = 0);
        long GetLong(string key, long @default = 0);
        float GetFloat(string key, float @default = 0);
        string GetString(string key, string def = null);
        bool GetBool(string key, bool def = false);
        T GetEnum<T>(string key, T val) where T : struct, IComparable, IConvertible, IFormattable;
        T GetJson<T>(string key);
        void GetJsonOverride<T>(string key, T inst);
        void Set(string key, string @default, bool save = true);
        void Set(string key, int @default, bool save = true);
        void Set(string key, long @default, bool save = true);
        void Set(string key, float @default, bool save = true);
        void Set(string key, bool @default, bool save = true);
        void SetEnum(string key, object @default, bool save = true);
        void SetJson(string key, object jsonObj, bool save = true);
        bool Exists(string key);
        void Delete(string key, bool save = true);
        void DeleteAll();
    }
}


