//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Collections.Generic;
using System.Ex;
using System.Reflection;
using System.Text.RegularExpressions;

namespace mulova.commons
{
    public class MethodCaller
    {
        class CallInfo
        {
            public readonly MethodInfo method;
            public readonly string arg;

            public CallInfo(string className, string methodName, string arg)
            {
                this.arg = arg;
                var cls = TypeEx.GetType(className);
                if (cls == null)
                {
                    throw new ArgumentException("Missing class " + className);
                }
                method = arg != null ? cls.GetMethod(methodName, new[] { typeof(string) }) : cls.GetMethod(methodName);
                if (method == null)
                {
                    throw new ArgumentException("Missing method " + methodName);
                }
            }

            public object Call()
            {
                if (arg != null)
                {
                    return method.Invoke(null, new object[] { arg });
                }
                else
                {
                    return method.Invoke(null, null);
                }
            }
        }

        private readonly Dictionary<string, CallInfo> cache;

        public MethodCaller(bool useCache)
        {
            if (useCache)
            {
                cache = new Dictionary<string, CallInfo>();
            }
        }

        public object CallMethod(string methodCall)
        {
            if (string.IsNullOrWhiteSpace(methodCall))
            {
                return null;
            }
            if (cache == null || !cache.TryGetValue(methodCall, out var callInfo))
            {
                var (className, methodName, arg) = ParseMethodCall(methodCall);
                callInfo = new CallInfo(className, methodName, arg);
                if (cache != null)
                {
                    cache[methodCall] = callInfo;
                }
            }
            return callInfo.Call();
        }

        public static (string className, string method, string arg) ParseMethodCall(string methodCall)
        {
            Regex regex = new Regex(@"(?<class>.*)\.(?<method>[^.()]+)(\((?<arg>.*)\))?");
            var match = regex.Match(methodCall);
            if (match != null)
            {
                var cls = match.Groups["class"];
                var method = match.Groups["method"];
                var arg = match.Groups["arg"];
                if (!cls.Success || !method.Success)
                {
                    throw new ArgumentException("Invalid method call " + methodCall);
                }
                return (cls.Captures[0].Value, method.Captures[0].Value, arg.Success ? arg.Captures[0].Value : null);
            }
            else
            {
                throw new ArgumentException("Invalid method call " + methodCall);
            }
        }
    }

}
