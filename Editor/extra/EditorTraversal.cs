using System;
using System.Collections.Generic;
using System.Ex;
using System.Text;
using System.Text.Ex;
using System.Text.RegularExpressions;
using mulova.commons;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace mulova.unicore
{
    public static class EditorTraversal
    {
        public static readonly ILog log = LogManager.GetLogger(nameof(EditorTraversal));
        private static EditorBuildSettingsScene sceneProcessing;

        private static Regex ignoreRegex;
        public static string ignorePattern = ".meta$|.fbx$|.FBX$|/Editor/|Assets/Plugins/";

        private static Regex ignorePath
        {
            get
            {
                if (ignoreRegex == null)
                {
                    ignoreRegex = new Regex(ignorePattern);
                }
                return ignoreRegex;
            }
        }

        public static void AddIgnorePattern(string regexPattern)
        {
            ignorePattern = string.Format("{0}|{1}", ignorePattern, regexPattern);
            ignoreRegex = null;
        }

        /// <summary>
        /// Fors the each scene.
        /// </summary>
        /// <returns>The each scene.</returns>
        /// <param name="func">[param] scene roots,  [return] error string</param>
        public static void ForEachScene(Func<Scene, string> func)
        {
            //      string current = EditorSceneBridge.currentScene;
            StringBuilder err = new StringBuilder();
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            for (int i=0; i<scenes.Length; ++i)
            {
                try
                {
                    ProcessScene(scenes[i], func);
                } catch (Exception ex)
                {
                    err.AppendLine(ex.ToString());
                }
            }
            if (err.Length > 0)
            {
                throw new Exception(err.ToString());
            }
        }

        public static List<string> ProcessScene(EditorBuildSettingsScene s, Func<Scene, string> func)
        {
            List<string> errors = new List<string>(); 
            sceneProcessing = s;

            try
            {
                var scene = EditorSceneManager.OpenScene(s.path);
                log.Debug("[Scene] '{0}'", scene.path);
                string error = func(scene);
                if (error.IsEmpty())
                {
                    EditorUtil.SaveScene();
                } else
                {
                    errors.Add(error);
                }
            } catch (Exception ex)
            {
                log.Error(ex);
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Error", "See editor log for details", "OK");
                throw;
            }

            sceneProcessing = null;
            return errors;
        }

        public static void ForEachAssetImporter<T>(string folderPath, string[] exts, Func<T, bool> action) where T : AssetImporter
        {
            var files = EditorAssetUtil.ListAssetPaths(folderPath, exts);
            int count = 0;

            try
            {
                foreach (var file in files)
                {
                    count++;
                    string title = string.Format("{0} ({1}/{2})", folderPath, count, files.Length);
                    if (EditorUtility.DisplayCancelableProgressBar(title, file, count / (float)files.Length))
                    {
                        throw new OperationCanceledException();
                    }
                    T im = AssetImporter.GetAtPath(file) as T;
                    if (im != null)
                    {
                        try
                        {
                            if (action(im))
                            {
                                im.SaveAndReimport();
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                        }
                    }
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        public static void ForEachAssetPath(string path, FileType fileType, params Action<string>[] funcs)
        {
            StringBuilder err = new StringBuilder();
            string[] paths = EditorAssetUtil.ListAssetPaths(path, fileType, true);
            log.Debug("Prebuild {0:D0} assets", paths.Length);
            for (int i=0; i<funcs.Length; ++i)
            {
                for (int j = 0; j < paths.Length; ++j)
                {
                    string p = "Assets/"+paths[j];
                    if (ignorePath.IsMatch(p))
                    {
                        continue;
                    }
                    if (EditorUtil.DisplayProgressBar($"Assets {i+1}/{funcs.Length}", p, j / (float)paths.Length))
                    {
                        throw new OperationCanceledException("canceled by user");
                    }
                    try {
                        funcs[i](p);
                    } catch (Exception ex)
                    {
                        log.Error(ex);
                        EditorUtility.ClearProgressBar();
                        EditorUtility.DisplayDialog("Error", "See editor log for details", "OK");
                        throw;
                    }
                }
            }
            EditorUtility.ClearProgressBar();
            if (err.Length > 0)
            {
                throw new Exception(err.ToString());
            } else
            {
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// Fors the each resource.
        /// </summary>
        /// <returns>The each resource.</returns>
        /// <param name="func">Func returns error message if occurs.</param>
        public static void ForEachAsset<T>(FileType fileType, Action<string, T> func)  where T:Object
        {
            ForEachAssetPath("Assets", fileType, p =>
            {
                T asset = AssetDatabase.LoadAssetAtPath<T>(p);
                func(p, asset);
            });
        }
    }
}

