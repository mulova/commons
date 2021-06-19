namespace mulova.unicore
{
    public interface IPreferenceImpl
    {
        int GetInt(string key, int @default = 0);
        float GetFloat(string key, float @default = 0);
        string GetString(string key, string @default = null);
        void Set(string key, int value);
        void Set(string key, float value);
        void Set(string key, string value);
        bool Exists(string key);
        void Delete(string key);
        void DeleteAll();
        void Save();
    }
}