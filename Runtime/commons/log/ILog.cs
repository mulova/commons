//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;

namespace mulova.commons
{
	public interface ILog
	{
        object context { get; set; }
        LogLevel level { get; set; }

        void Debug(string format, object arg1);
        void Debug(string format, object arg1, object arg2);
        void Debug(string format, object arg1, object arg2, object arg3);
        void Debug(string format, params object[] args);

        void Warn(string format, object arg1);
        void Warn(string format, object arg1, object arg2);
        void Warn(string format, object arg1, object arg2, object arg3);
        void Warn(string format, params object[] args);
		void Warn(Exception e, string format = null, params object[] args);

        void Error(string format, object arg1);
        void Error(string format, object arg1, object arg2);
        void Error(string format, object arg1, object arg2, object arg3);
        void Error(string format, params object[] args);
		void Error(Exception e, string format = null, params object[] args);

        void AddAppender(LogAppender a);
		void RemoveAppender(LogAppender a);
		void SetFormatter(LogFormatter f);
		bool IsLoggable(LogLevel level);
	}
    /*
    public interface ILogger : ILogHandler
    {
        /// <summary>
        ///   <para>Set Logger.ILogHandler.</para>
        /// </summary>
        ILogHandler logHandler
        {
            get;
            set;
        }

        /// <summary>
        ///   <para>To runtime toggle debug logging [ON/OFF].</para>
        /// </summary>
        bool logEnabled
        {
            get;
            set;
        }

        /// <summary>
        ///   <para>To selective enable debug log message.</para>
        /// </summary>
        LogType filterLogType
        {
            get;
            set;
        }

        /// <summary>
        ///   <para>Check logging is enabled based on the LogType.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <returns>
        ///   <para>Retrun true in case logs of LogType will be logged otherwise returns false.</para>
        /// </returns>
        bool IsLogTypeAllowed(LogType logType);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(LogType logType, object message);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(LogType logType, object message, Object context);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(LogType logType, string tag, object message);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(LogType logType, string tag, object message, Object context);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(object message);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(string tag, object message);

        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="tag"></param>
        void Log(string tag, object message, Object context);

        /// <summary>
        ///   <para>A variant of Logger.Log that logs an warning message.</para>
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        void LogWarning(string tag, object message);

        /// <summary>
        ///   <para>A variant of Logger.Log that logs an warning message.</para>
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        void LogWarning(string tag, object message, Object context);

        /// <summary>
        ///   <para>A variant of ILogger.Log that logs an error message.</para>
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        void LogError(string tag, object message);

        /// <summary>
        ///   <para>A variant of ILogger.Log that logs an error message.</para>
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        void LogError(string tag, object message, Object context);

        /// <summary>
        ///   <para>Logs a formatted message.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void LogFormat(LogType logType, string format, params object[] args);

        /// <summary>
        ///   <para>A variant of ILogger.Log that logs an exception message.</para>
        /// </summary>
        /// <param name="exception"></param>
        void LogException(Exception exception);
    }

    public interface ILogHandler
    {
        /// <summary>
        ///   <para>Logs a formatted message.</para>
        /// </summary>
        /// <param name="logType">The type of the log message.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        void LogFormat(LogType logType, Object context, string format, params object[] args);

        /// <summary>
        ///   <para>A variant of ILogHandler.LogFormat that logs an exception message.</para>
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        /// <param name="context">Object to which the message applies.</param>
        void LogException(Exception exception, Object context);
    }
*/
}
