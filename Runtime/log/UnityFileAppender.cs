//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.IO;
using System.IO.Ex;
using mulova.commons;

namespace mulova.unicore
{
    public class UnityFileAppender : LogAppender {
        
        private StreamWriter writer;
        
        public void Write (ILog logger, LogLevel level, object message){
            if (writer == null) {
                DirectoryInfo dir = null;
                if (Platform.isEditor) {
                    dir = new DirectoryInfo(Platform.dataPath);
                    dir = dir.Parent;
                    dir = new DirectoryInfo(Path.Combine(dir.FullName, "log"));
                } else {
                    dir = new DirectoryInfo(Path.Combine(Platform.persistentDataPath, "log"));
                }
                FileStream file = dir.CreateUniqueFile("log.txt");
                writer = new StreamWriter(file);
            }
            
            writer.WriteLine(message);
            writer.Flush();
        }
        
        public void Cleanup() {
            writer.Close();
        }
    }
}
