//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.IO;
using UnityEngine;
using UnityEngine.Ex;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace mulova.unicore
{
    public static class Platform
    {
        public static SimulationMode simulationMode = SimulationMode.Editor;
        private static bool debug;
        private static bool playing;
        private static bool editor;
        private static string persistentPath;
        private static string dlPath;
        private static string streamingPath;
        private static string tempCachePath;
        private static string path;
        private static bool init;

        public static bool IsInitialized()
        {
            return init;
        }

        public static void Init()
        {
            try
            {
                if (!init)
                {
                    playing = Application.isPlaying;
                    editor = Application.isEditor;
                    persistentPath = Application.persistentDataPath;
                    tempCachePath = Application.temporaryCachePath;
                    path = Application.dataPath;
                    debug = Debug.isDebugBuild;
                    streamingPath = editor ? Path.Combine(path, "StreamingAssets") : Application.streamingAssetsPath;
                    if (playing)
                    {
                        init = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError(ex);
            }
        }

        public static bool isPlaying
        {
            get
            {
                Init();
                return playing;
            }
        }

        /// <summary>
        /// Gets the persistent data path.
        /// Don't use this in Constructor
        /// </summary>
        /// <value>The persistent data path.</value>
        public static string persistentDataPath
        {
            get
            {
                Init();
                return persistentPath;
            }
        }

        public static string streamingAssetsPath
        {
            get
            {
                Init();
                return streamingPath;
            }
        }

        public static string temporaryCachePath
        {
            get
            {
                Init();
                return tempCachePath;
            }
        }

        public static string downloadPath
        {
            get
            {
                Init();
                if (dlPath == null)
                {
                    if (platform.IsIos())
                    {
                        return temporaryCachePath;
                    }
                    else
                    {
                        return persistentDataPath;
                    }
                }
                return dlPath;
            }
            set
            {
                dlPath = value;
            }
        }

        /// <summary>
        /// Gets the data path.
        /// Don't use this in Constructor
        /// </summary>
        /// <value>The data path.</value>
        public static string dataPath
        {
            get
            {
                Init();
                return path;
            }
        }

        /// <summary>
        /// Don't use this in Constructor
        /// </summary>
        /// <value><c>true</c> if is build; otherwise, <c>false</c>.</value>
        public static bool isBuild
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return !isEditor;
            }
        }

        /// <summary>
        /// Don't use this in Constructor
        /// </summary>
        /// <value><c>true</c> if is editor; otherwise, <c>false</c>.</value>
        public static bool isEditor
        {
            get
            {
                Init();
                if (!editor)
                {
                    return false;
                }
                return !simulationMode.IsBuild();
            }
        }

        public static bool isReleaseBuild
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (simulationMode == SimulationMode.ReleaseBuild)
                {
                    return true;
                }
                return isBuild && !debug;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Platform"/> is not release build.
        /// </summary>
        /// <value><c>true</c> if current status is not release build otherwise, <c>false</c>.</value>
        public static bool isDebug
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return !isReleaseBuild;
            }
        }

        private static Properties _conf;
        public static Properties conf
        {
            get
            {
                if (_conf == null)
                {
                    _conf = new Properties();
                    _conf.LoadResource("platform_config");
                }
                return _conf;
            }
        }

        public static void Reset()
        {
            init = false;
            _conf = null;
        }

        public static RuntimePlatform platform
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if UNITY_ANDROID
                return RuntimePlatform.Android;
#elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
#elif UNITY_STANDALONE_OSX
                return RuntimePlatform.OSXPlayer;
#elif UNITY_STANDALONE_WIN
                return RuntimePlatform.WindowsPlayer;
#elif UNITY_STANDALONE_LINUX
                return RuntimePlatform.LinuxPlayer;
#elif UNITY_WII
                return RuntimePlatform.Switch;
#elif UNITY_PS4
                return RuntimePlatform.PS4;
#elif UNITY_XBOXONE
                return RuntimePlatform.XboxOne;
#elif UNITY_TIZEN
                return RuntimePlatform.TizenPlayer;
#elif UNITY_TVOS
                return RuntimePlatform.tvOS;
#elif UNITY_WEBGL
                return RuntimePlatform.WebGLPlayer;
#else
                return Application.platform;
#endif
            }
        }
    }
}
