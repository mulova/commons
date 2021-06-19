using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic.Ex;
using System.Ex;
using System.IO;
using System.Linq;
using System.Text.Ex;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.Ex;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace mulova.unicore
{
    public enum ObjCategory
    {
        NA, SceneInstance, PrefabInstance, Asset
    }

    [Serializable]
    public class ObjRef : IComparable<ObjRef>
    {
        [SerializeField] private string assetId;
        [SerializeField] private string _type;

        [SerializeField] private ObjCategory _category;
        [SerializeField] private string[] scenePathNames;
        [SerializeField] private int[] scenePathIndex;
        [NonSerialized] private int instanceId;
        // guid for asset, scene path for scene object
        public string assetGuid { get { return assetId; } private set { this.assetId = value; } }
        public SceneCamProperty cam;

        public string assetPath => AssetDatabase.GUIDToAssetPath(assetId);

        public ObjCategory category
        {
            get
            {
                if (_category == ObjCategory.NA)
                {
                    if (!scenePathNames.IsEmpty())
                    {
                        var o = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                        if (o is SceneAsset)
                        {
                            _category = ObjCategory.SceneInstance;
                        } else
                        {
                            _category = ObjCategory.PrefabInstance;
                        }
                    } else
                    {
                        _category = ObjCategory.Asset;
                    }
                } else
                {
                }
                return _category;
            }
        }

        public Type type
        {
            get
            {
                return !_type.IsEmpty() ? TypeEx.GetType(_type) : null;
            }
            private set
            {
                if (value != null)
                {
                    _type = value.FullName;
                } else
                {
                    _type = null;
                }
            }
        }

        public Object reference
        {
            get
            {
                if (category == ObjCategory.Asset)
                {
                    string path = assetPath;
                    if (!path.IsEmpty())
                    {
                        return AssetDatabase.LoadAssetAtPath(path, type);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Object obj = instanceId != 0 ? EditorUtility.InstanceIDToObject(instanceId) : null;
                    if (obj == null)
                    {
                        instanceId = GetInstanceId(assetId, scenePathIndex, scenePathNames);
                        //if (instanceId == 0)
                        //{
                        //    // invalidate
                        //    scenePathIndex = null;
                        //    scenePathNames = null;
                        //}
                    }
                    return obj;
                }
            }

            set
            {
                if (value != null)
                {
                    string path = AssetDatabase.GetAssetPath(value);
                    if (!path.IsEmpty())
                    {
                        assetId = AssetDatabase.AssetPathToGUID(path);
                        instanceId = 0;
                    }
                    else
                    {
                        var go = value.GetGameObject();
                        instanceId = go.GetInstanceID();
                        (_, scenePathNames, scenePathIndex) = go.transform.GetScenePath(null);
                        var stage = PrefabStageUtility.GetCurrentPrefabStage();
                        if (stage != null && value is GameObject && stage.IsPartOfPrefabContents(value as GameObject))
                        {
#if UNITY_2020_1_OR_NEWER
                            assetId = AssetDatabase.AssetPathToGUID(stage.assetPath);
#else
                            assetId = AssetDatabase.AssetPathToGUID(stage.prefabAssetPath);
#endif
                        } else
                        {
                            assetId = AssetDatabase.AssetPathToGUID(go.scene.path);
                        }
                    }
                    this.type = value.GetType();
                }
                else
                {
                    assetId = null;
                    instanceId = 0;
                    scenePathIndex = null;
                    scenePathNames = null;
                    type = null;
                }
            }
        }

        public static int GetInstanceId(string assetId, int[] scenePathIndex, string[] scenePathNames)
        {
            var id = 0;
            var asset = AssetDatabase.GUIDToAssetPath(assetId);
            if (asset != null)
            {
                if (!scenePathIndex.IsEmpty())
                {
                    var stage = PrefabStageUtility.GetCurrentPrefabStage();
                    if (stage != null)
                    {
                        var root = stage.prefabContentsRoot;
                        while (root.transform.parent != null)
                        {
                            root = root.transform.parent.gameObject;
                        }
                        Transform t = FindHierarchy(root, scenePathIndex, scenePathNames);
                        if (t != null)
                        {
                            id = t.gameObject.GetInstanceID();
                        }
                    } else
                    {
                        for (int i = 0; i < SceneManager.sceneCount; ++i)
                        {
                            var s = SceneManager.GetSceneAt(i);
                            if (s.path == asset)
                            {
                                var roots = s.GetRootGameObjects();
                                if (roots.Length <= scenePathIndex[0])
                                {
                                    break;
                                }
                                var root = roots[scenePathIndex[0]];
                                Transform t = FindHierarchy(root, scenePathIndex, scenePathNames);
                                if (t != null)
                                {
                                    id = t.gameObject.GetInstanceID();
                                }
                            }
                        }
                    }
                }
            }
            return id;
        }

        public static Transform FindHierarchy(GameObject root, int[] scenePathIndex, string[] scenePathNames)
        {
            var t = root.transform;
            if (root.name == scenePathNames[0])
            {
                for (int j = 1; j < scenePathIndex.Length; ++j)
                {
                    if (t.childCount <= scenePathIndex[j])
                    {
                        t = null;
                        break;
                    }
                    t = t.GetChild(scenePathIndex[j]);
                    if (t.name != scenePathNames[j])
                    {
                        t = null;
                        break;
                    }
                }
            }

            return t;
        }

        public string path
        {
            get
            {
                if (category == ObjCategory.Asset)
                {
                    return AssetDatabase.GUIDToAssetPath(assetGuid);
                } else
                {
                    var r = reference;
                    if (r != null)
                    {
                        return r.GetGameObject().transform.GetScenePath();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public ObjRef(Object o)
        {
            if (o != null)
            {
                reference = o;
            }
        }


        public override bool Equals(object obj)
        {
            ObjRef that = obj as ObjRef;
            if (that != null)
            {
                return this.assetGuid == that.assetGuid || this.instanceId == that.instanceId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var code = assetId.GetHashCode();
            if (instanceId != 0)
            {
                code = Hashing(code, instanceId);
            }
            if (scenePathIndex != null)
            {
                code = Hashing(code, ((IStructuralEquatable)scenePathIndex).GetHashCode(EqualityComparer<int>.Default));
            }
            return code;
        }

        private int Hashing(int c1, int c2)
        {
            return c1 << 5 - c1 + c2;
        }

        public override string ToString()
        {
            var assetName = Path.GetFileNameWithoutExtension(assetPath);

            if (scenePathNames != null)
            {
                return $"{assetName}/.../{scenePathNames.Last()}";
            }
            return assetName;
        }

        public int CompareTo(ObjRef that)
        {
            var d = this.assetId.CompareTo(that.assetId);
            if (d != 0)
            {
                return d;
            }
            if (this.scenePathIndex != null && that.scenePathIndex != null)
            {
                for (int i=0; i<scenePathIndex.Length; ++i)
                {
                    if (i < that.scenePathIndex.Length)
                    {
                        d = this.scenePathIndex[i] - that.scenePathIndex[i];
                        if (d != 0)
                        {
                            return d;
                        }
                    }
                }
                d = this.scenePathIndex.Length - that.scenePathIndex.Length;
                if (d != 0)
                {
                    return d;
                }
            }
            return d;
        }
    }
}
