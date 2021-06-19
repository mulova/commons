using UnityEditor;
using UnityEngine;
using mulova.commons;

namespace mulova.unicore
{
    public class ScriptableObjectGen
    {
        [MenuItem("Assets/Create/ScriptableObject")]
        public static void GenerateScriptableObject() {
            Object scriptableObj = Selection.activeObject;
            string selPath = AssetDatabase.GetAssetPath(scriptableObj);
            string path = PathUtil.GetDirectory(selPath);
            EditorAssetUtil.CreateScriptableObject(scriptableObj.name, path+scriptableObj.name+".asset");
        }
        
        [MenuItem("Assets/Create/ScriptableObject", true)]
        public static bool IsGenerateScriptableObject() {
            if (Selection.activeObject != null && Selection.activeObject.GetType() == typeof(MonoScript)) {
                MonoScript script = Selection.activeObject as MonoScript;
                var cls = script.GetClass();
                if (cls != null)
                {
                    return cls.IsSubclassOf(typeof(ScriptableObject)) && !cls.IsSubclassOf(typeof(Editor));
                }
            }
            return false;
        }
    }
}


