namespace mulova.unicore
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// From NGUIEditorTools
    /// </summary>
    public class EditorUI
    {
        public class ContentArea : IDisposable
        {
            public readonly bool foldout;

            public ContentArea(string text, int fontSize = 11, string prefKey = null, Action drawAdditional = null)
            {
                foldout = DrawHeader(text, fontSize, prefKey, drawAdditional);
                if (foldout)
                {
                    EditorGUI.indentLevel++;
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(4f);
                    GUILayout.BeginHorizontal("TextArea", GUILayout.MinHeight(10f));
                    GUILayout.BeginVertical();
                    GUILayout.Space(2f);
                }
            }

            public void Dispose()
            {
                if (foldout)
                {
                    GUILayout.Space(3f);
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                    GUILayout.Space(3f);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(3f);
                    EditorGUI.indentLevel--;
                }
            }

            public static implicit operator bool(ContentArea a)
            {
                return a.foldout;
            }
        }

        public class ColorScope : IDisposable
        {
            private Color old;
            public ColorScope(Color c, bool apply = true)
            {
                old = GUI.color;
                if (apply)
                {
                    GUI.color = c;
                }
            }

            public void Dispose()
            {
                GUI.color = old;
            }
        }

        public class HorizontalScope : IDisposable
        {
            public HorizontalScope()
            {
                EditorGUILayout.BeginHorizontal();
            }

            public void Dispose()
            {
                EditorGUILayout.EndHorizontal();
            }
        }
        /// <summary>
        /// Draw a distinctly different looking header label
        /// </summary>
        static private Dictionary<string, bool> foldouts = new Dictionary<string, bool>();

        static public bool DrawHeader(string text, int fontSize = 11, string prefKey = null, Action drawAdditional = null)
        {
            var pref = !string.IsNullOrEmpty(prefKey);
            var state = true;
            if (pref)
            {
                state = EditorPrefs.GetBool(prefKey, true);
            }
            else
            {
                if (!foldouts.TryGetValue(text, out state))
                {
                    state = true;
                }
            }
            GUILayout.Space(3f);
            if (!state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(3f);

            var arrow = state ? "\u25BE" : "\u25B8";
            var richText = $"<b><size={fontSize}>{arrow} {text}</size></b>";
            var changed = GUI.changed;
            var newState = GUILayout.Toggle(state, richText, "dragtab", GUILayout.MinWidth(20f));
            GUI.changed = changed;
            if (state != newState)
            {
                state = newState;
                if (pref)
                {
                    EditorPrefs.SetBool(prefKey, state);
                }
                else
                {
                    foldouts[text] = state;
                }
            }

            GUILayout.Space(2f);
            drawAdditional?.Invoke();
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            if (!state) GUILayout.Space(3f);
            return state;
        }

        /// <summary>
        /// Create an undo point for the specified objects.
        /// </summary>

        static public void RegisterUndo(string name, params Object[] objects)
        {
            if (objects != null && objects.Length > 0)
            {
                UnityEditor.Undo.RecordObjects(objects, name);
                foreach (Object obj in objects)
                {
                    if (obj == null) continue;
                    SetDirty(obj);
                }
            }
        }

        /// <summary>
        /// Convenience function that converts Class + Function combo into Class.Function representation.
        /// </summary>

        static public string GetFuncName(object obj, string method)
        {
            if (obj == null) return "<null>";
            if (string.IsNullOrEmpty(method)) return "<Choose>";
            string type = obj.GetType().ToString();
            int period = type.LastIndexOf('.');
            if (period > 0) type = type.Substring(period + 1);
            return type + "." + method;
        }

        public static bool GUIDField<T>(Rect rect, string label, ref string guid) where T : Object
        {
            T o = null;
            string assetPath = !string.IsNullOrEmpty(guid) ? AssetDatabase.GUIDToAssetPath(guid) : null;
            if (!string.IsNullOrEmpty(assetPath))
            {
                o = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
            T newObj = EditorGUI.ObjectField(rect, label, o, typeof(T), false) as T;
            if (newObj != o)
            {
                guid = newObj != null ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(newObj)) : null;
                return true;
            }
            return false;
        }

        public static void SetDirty(Object o)
        {
            if (Application.isPlaying || o == null)
            {
                return;
            }
            GameObject go = null;
            if (o is GameObject)
            {
                go = o as GameObject;
            }
            else if (o is Component)
            {
                go = (o as Component).gameObject;
            }
            if (go != null && go.scene.IsValid())
            {
                EditorSceneManager.MarkSceneDirty(go.scene);
            }
            else
            {
                EditorUtility.SetDirty(o);
            }
        }
    }
}
