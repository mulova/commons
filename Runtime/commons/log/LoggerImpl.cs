//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.Ex;
using System.Collections.Generic.Ex;

namespace mulova.commons
{
	class LoggerImpl : ILog
	{
        public object context { get; set; }
        public LogLevel level { get; set; }
        private LogFormatter formatter;
        private List<LogAppender> appenders = new List<LogAppender>();
		private string name;
		
        public LoggerImpl(string name, LogFormatter formatter){
			this.name = name;
            this.formatter = formatter;
        }

        public void SetFormatter(LogFormatter f)
        {
            formatter = f;
		}
        
        public void AddAppender(LogAppender a) {
        	appenders.Add(a);
		}
		
		public void RemoveAppender(LogAppender a) {
			appenders.Remove(a);
		}
		
		public void SetAppenders(IEnumerable<LogAppender> a) {
			appenders.Clear();
			appenders.AddRange(a);
		}

        public void SetContext(object ctx)
        {
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Debug(string format, object arg1)
        {
            WriteContext(LogLevel.Log, null, format, arg1);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Debug(string format, object arg1, object arg2)
        {
            WriteContext(LogLevel.Log, null, format, arg1, arg2);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Debug(string format, object arg1, object arg2, object arg3)
        {
            WriteContext(LogLevel.Log, null, format, arg1, arg2, arg3);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Debug(string format, params object[] data)
        {
            WriteContext(LogLevel.Log, null, format, data);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Warn(string format, object arg1)
        {
            WriteContext(LogLevel.Warning, null, format, arg1);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Warn(string format, object arg1, object arg2)
        {
            WriteContext(LogLevel.Warning, null, format, arg1, arg2);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Warn(string format, object arg1, object arg2, object arg3)
        {
            WriteContext(LogLevel.Warning, null, format, arg1, arg2, arg3);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Warn(string format, params object[] data)
        {
            WriteContext(LogLevel.Warning, null, format, data);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Warn(Exception e, string format = null, params object[] data)
		{
			WriteContext(LogLevel.Warning, e, format, data);
		}
        [DebuggerHidden, DebuggerStepThrough]
        public void Error(string format, object arg1)
        {
            WriteContext(LogLevel.Error, null, format, arg1);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Error(string format, object arg1, object arg2)
        {
            WriteContext(LogLevel.Error, null, format, arg1, arg2);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Error(string format, object arg1, object arg2, object arg3)
        {
            WriteContext(LogLevel.Error, null, format, arg1, arg2, arg3);
        }
        [DebuggerHidden, DebuggerStepThrough]
        public void Error(string format, params object[] data)
        {
            WriteContext(LogLevel.Error, null, format, data);
        }

        [DebuggerHidden, DebuggerStepThrough]
        public void Error(Exception e, string format = null, params object[] data)
		{
			WriteContext(LogLevel.Error, e, format, data);
		}

        public void SetLevel(LogLevel level)
        {
            this.level = level;
        }
		public LogLevel GetLevel() {
			return level;
		}
		public bool IsLoggable(LogLevel l) {
			return level.IsLoggable(l);
		}

        [DebuggerHidden, DebuggerStepThrough]
        private void WriteContext(LogLevel lv, Exception e, string format, object param1)
        {
            if (this.level > lv) return;
            WriteContext(lv, e, format, new object[]{param1});
        }

        [DebuggerHidden, DebuggerStepThrough]
        private void WriteContext(LogLevel lv, Exception e, string format, object param1, object param2)
        {
            if (this.level > lv) return;
            WriteContext(lv, e, format, new object[]{param1, param2});
        }

        [DebuggerHidden, DebuggerStepThrough]
        private void WriteContext(LogLevel lv, Exception e, string format, object param1, object param2, object param3)
        {
            if (this.level > lv) return;
            WriteContext(lv, e, format, new object[]{param1, param2, param3});
        }

        [DebuggerHidden, DebuggerStepThrough]
        private void WriteContext(LogLevel lv, Exception e, string format, params object[] data)
        {
            if (this.level > lv) return;
            
            string message = string.Empty;
			if (!format.IsEmpty()) {
				try {
					if (!data.IsEmpty()) {
						message = string.Format(format, data);
					} else {
						message = format;
					}
				} catch (Exception ex) {
					foreach (LogAppender a in appenders) {
						a.Write(this, LogLevel.Error, string.Join("\n", new string[] {format, ex.Message, ex.StackTrace}));
					}
				}
			}
            if (formatter != null) {
				message = formatter.Format(this, lv, message, e);
			}
			
			foreach (LogAppender a in appenders) {
				a.Write(this, lv, message);
			}
        }
		
		public string Name {
			get { return name; }
		}
		
		public override string ToString() {
			return name;
		}
        
	}
}
