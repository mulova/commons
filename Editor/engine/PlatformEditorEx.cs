using UnityEditor;
using UnityEngine;

namespace mulova.unicore
{
    public static class PlatformEditorEx
    {
        public static RuntimePlatform GetPlatform()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneOSX:
                    return RuntimePlatform.OSXPlayer;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return RuntimePlatform.WindowsPlayer;
                case BuildTarget.iOS:
                    return RuntimePlatform.IPhonePlayer;
                case BuildTarget.Android:
                    return RuntimePlatform.Android;
                case BuildTarget.StandaloneLinux64:
                    return RuntimePlatform.LinuxPlayer;
                case BuildTarget.WebGL:
                    return RuntimePlatform.WebGLPlayer;
                default:
                    return RuntimePlatform.Android;
            }
        }
    }
}

