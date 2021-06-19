//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System.Text;
using System.Text.Ex;
using mulova.commons;
using UnityEngine;

namespace mulova.unicore
{
    public class UnityConsoleAppender : LogAppender {
        
        public int fontSize = 0;
        public bool bold;
        public string color;

        [RuntimeInitializeOnLoadMethod]
        private static void AutoAppend()
        {
            LogManager.AddAppender(new UnityConsoleAppender());
        }

        private StringBuilder str = new StringBuilder(10240);
        public void Write(ILog logger, LogLevel level, object message){
            if (message == null) {
                return;
            }
            str.Length = 0;
            if (fontSize > 0) {
                str.Append("<size=").Append(fontSize).Append(">");
            }
            if (!color.IsEmpty()) {
                str.Append("<color=").Append(color).Append(">");
            }
            if (bold) {
                str.Append("<b>");
            }
            if (str.Length > 0) {
                str.Append(message.ToString());
                if (bold) {
                    str.Append("</b>");
                }
                if (!color.IsEmpty()) {
                    str.Append("</color>");
                }
                if (fontSize > 0) {
                    str.Append("</size>");
                }
                message = str.ToString();
            }

            if (level == LogLevel.Error) {
                Debug.LogError(message, logger.context as Object);
            } else if (level == LogLevel.Warning) {
                Debug.LogWarning(message, logger.context as Object);
            } else {
                Debug.Log(message, logger.context as Object);
            }
        }
        
        public void Cleanup() { }
    }
}

