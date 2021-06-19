//----------------------------------------------
// Common library for Unity3D libraries
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

namespace UnityEngine.Ex
{
    public static class RuntimePlatformEx
    {
        public static bool IsMobile(this RuntimePlatform platform)
        {
            return platform == RuntimePlatform.Android
                || platform == RuntimePlatform.IPhonePlayer;
        }

        public static bool IsStandalone(this RuntimePlatform platform)
        {
            return platform.IsOSX()
                || platform.IsWindows()
                || platform.IsLinux();
        }

        public static bool IsIos(this RuntimePlatform platform)
        {
            return platform == RuntimePlatform.IPhonePlayer;
        }

        public static bool IsOSX(this RuntimePlatform platform)
        {
            return platform == RuntimePlatform.OSXPlayer
                || platform == RuntimePlatform.OSXEditor;
        }

        public static bool IsWindows(this RuntimePlatform platform)
        {
            return platform == RuntimePlatform.WindowsEditor
                || platform == RuntimePlatform.WindowsPlayer;
        }

        public static bool IsLinux(this RuntimePlatform platform)
        {
            return platform == RuntimePlatform.LinuxPlayer;
        }

        public static bool IsWeb(this RuntimePlatform platform)
        {
            return platform == RuntimePlatform.WebGLPlayer;
        }

        public static string GetTargetName(this RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.Android:
                    return "android";
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "osx";
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "win64";
                case RuntimePlatform.IPhonePlayer:
                    return "ios";
                case RuntimePlatform.LinuxPlayer:
                    return "linux64";
                case RuntimePlatform.WebGLPlayer:
                    return "webgl";
                default:
                    return "none";
            }
        }
    }
}


