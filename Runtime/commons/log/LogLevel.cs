//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------


namespace mulova.commons
{
    public enum LogLevel
    {
        Log,
        Warning,
        Error,
        Exception,
        Assert
    }

    public static class LogLevelEx {
		public static bool IsLoggable(this LogLevel srcLevel, LogLevel dstLevel) {
			return (int)srcLevel <= (int)dstLevel;
		}
	}
}
