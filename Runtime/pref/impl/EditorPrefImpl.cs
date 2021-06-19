namespace mulova.unicore
{
    [System.Diagnostics.DebuggerStepThrough]
    internal class EditorPrefImpl : IPreferenceImpl
    {
        private readonly string prefix;

        public EditorPrefImpl(string prefix)
        {
            this.prefix = prefix;
        }

        private string GetPrefixed(string id)
        {
            return string.IsNullOrEmpty(prefix) ? id : prefix + id;
        }
#if UNITY_EDITOR
        public void Delete(string key) => UnityEditor.EditorPrefs.DeleteKey(GetPrefixed(key));
        public void DeleteAll() => UnityEditor.EditorPrefs.DeleteAll();
        public bool Exists(string key) => UnityEditor.EditorPrefs.HasKey(GetPrefixed(key));
        public float GetFloat(string key, float @default = 0) => UnityEditor.EditorPrefs.GetFloat(GetPrefixed(key), @default);
        public int GetInt(string key, int @default = 0) => UnityEditor.EditorPrefs.GetInt(GetPrefixed(key), @default);
        public string GetString(string key, string @default = null) => UnityEditor.EditorPrefs.GetString(GetPrefixed(key), @default);
        public void Set(string key, int value) => UnityEditor.EditorPrefs.SetInt(GetPrefixed(key), value);
        public void Set(string key, float value) => UnityEditor.EditorPrefs.SetFloat(GetPrefixed(key), value);
        public void Set(string key, string value) => UnityEditor.EditorPrefs.SetString(GetPrefixed(key), value);
        public void Save() { }
#else
    public float GetFloat(string key, float @default = 0) => UnityEngine.PlayerPrefs.GetFloat(key, @default);
    public int GetInt(string key, int @default = 0) => UnityEngine.PlayerPrefs.GetInt(key, @default);
    public string GetString(string key, string @default = null) => UnityEngine.PlayerPrefs.GetString(key, @default);
    public void Set(string key, int value) => UnityEngine.PlayerPrefs.SetInt(key, value);
    public void Set(string key, float value) => UnityEngine.PlayerPrefs.SetFloat(key, value);
    public void Set(string key, string value) => UnityEngine.PlayerPrefs.SetString(key, value);
    public void Delete(string key) => UnityEngine.PlayerPrefs.DeleteKey(key);
    public void DeleteAll() => UnityEngine.PlayerPrefs.DeleteAll();
    public bool Exists(string key) => UnityEngine.PlayerPrefs.HasKey(key);
    public void Save() => UnityEngine.PlayerPrefs.Save();
#endif
    }
}