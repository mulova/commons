using System.IO;
using System.Text.Ex;
using System.Text;

namespace mulova.commons
{
    public static partial class AssetUtil
    {
        public static void CopyDir(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDir(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        /**
         * @wildcard filtering정보. null이면 모두를 반환한다. 예) *.fbx
         */
        public static FileInfo[] ListFiles(string absolutePath, string wildcard = null)
        {
            if (Directory.Exists(absolutePath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(absolutePath);
                
                if (wildcard != null)
                {
                    return dirInfo.GetFiles(wildcard, SearchOption.AllDirectories);
                } else
                {
                    return dirInfo.GetFiles("*", SearchOption.AllDirectories);
                }
            } else if (File.Exists(absolutePath))
            {
                return new FileInfo[] { new FileInfo(absolutePath) };
            } else
            {
                return new FileInfo[0];
            }
        }

        public static string[] GetDirectories(string rootDir)
        {
            if (Directory.Exists(rootDir))
            {
                return Directory.GetDirectories(rootDir);
            } else
            {
                return new string[0];
            }
        }

        public static void DeleteDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            foreach (FileInfo f in dir.GetFiles())
            {
                f.Delete(); 
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                d.Delete(true); 
            }
            Directory.Delete(dirPath);
        }

		public static void WriteAllText(string path, string text, Encoding enc)
		{
			string dir = PathUtil.GetDirectory(path);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			File.WriteAllText(path, text, enc);
		}

        public static void Move(string oldPath, string newPath)
        {
            if (!File.Exists(oldPath))
            {
                return;
            }
            string dir = PathUtil.GetDirectory(newPath);
            if (!Directory.Exists(dir) && !dir.IsEmpty())
            {
                Directory.CreateDirectory(dir);
            }
            File.Move(oldPath, newPath);
        }

    }

}
