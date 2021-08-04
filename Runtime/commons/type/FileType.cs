//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Ex;

namespace mulova.commons
{
    [Flags]
    public enum FileType
    {
        // unity format
        Prefab           = 0b0000000000000000000000000000001,
        Asset            = 0b0000000000000000000000000000010,
        Scene            = 0b0000000000000000000000000000100,
        Anim             = 0b0000000000000000000000000001000,
        Model            = 0b0000000000000000000000000010000,
        Image            = 0b0000000000000000000000000100000,
        Audio            = 000000000000000000000000001000000,
        Video            = 0b0000000000000000000000010000000,
        Text             = 0b0000000000000000000000100000000,
        Material         = 0b0000000000000000000001000000000,
        Animator         = 0b0000000000000000000010000000000,
        Script           = 0b0000000000000000000100000000000,
        // unity don't recognize
        Zip              = 0b0100000000000000000000000000000,
        Meta             = 0b1000000000000000000000000000000,
        All              = 0b1111111111111111111111111111111,
    }

    public static class FileTypeEx
    {
        public static readonly FileType UNITY_SUPPORTED = (FileType)0b0011111111111111111111111111111;
        public static readonly FileType[] ALL = (FileType[])Enum.GetValues(typeof(FileType));

        private static string[][] EXT = new string[][] {
            new string[] { ".prefab" },
            new string[] { ".asset" },
            new string[] { ".unity" },
            new string[] { ".anim", ".playable" },
            new string[] { ".fbx" },
            new string[] { ".png", ".jpg", ".dds", ".tga", ".tiff", ".tif", ".psd" },
            new string[] { ".ogg", ".mp3", ".wav" },
            new string[] { ".mp4" },
            new string[] { ".txt", ".bytes", ".csv" },
            new string[] { ".mat" },
            new string[] { ".controller" },
            new string[] { ".cs" },
            new string[] { ".zip" },
            new string[] { ".meta" },
            new string[] { "" }
        };

        public static FileType GetFileType(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return FileType.All;
            }
            for (int i = 0; i < EXT.Length; i++)
            {
                foreach (string ext in EXT[i])
                {
                    if (path.EndsWithIgnoreCase(ext))
                    {
                        return (FileType)(1<<i);
                    }
                }
            }
            return FileType.All;
        }

        public static string[] GetExt(this FileType fileType)
        {
            if (fileType == FileType.All)
            {
                return EXT[EXT.Length-1];
            }
            List<string> list = new List<string>();
            for (int i = 0; i < EXT.Length-1; ++i)
            {
                if (((1<<(i-1))&(int)fileType) != 0)
                {
                    list.AddRange(EXT[i]);
                }
            }
            return list.ToArray();
        }
    }
}

