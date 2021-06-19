using System;
using System.IO;
using System.Text;
using mulova.commons;
using UnityEngine;

namespace mulova.unicore
{
    public static class JsonUtil
    {
        public static void WriteJson(string path, object o, Encoding enc)
        {
            var text = JsonUtility.ToJson(o);
            AssetUtil.WriteAllText(path, text, enc);
        }

        public static T ReadJson<T>(string path) where T : new()
        {
            if (File.Exists(path))
            {
                try
                {
                    var json = File.ReadAllText(path);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        return JsonUtility.FromJson<T>(json);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
            return new T();
        }

    }

}
