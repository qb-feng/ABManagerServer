using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FengHC.Log
{
    public class DebugManager
    {
        public static bool DebugMode = true;

        #region 常用log
        public static void Log(object message)
        {
            if (DebugMode)
                System.Console.WriteLine("Log:" + message);
        }
        public static void LogError(object message)
        {
            if (DebugMode)
                System.Console.WriteLine("LogError:" + message);
        }
        public static void LogWarning(object message)
        {
            if (DebugMode)
                System.Console.WriteLine("LogWarring:" + message);
        }
        #endregion

        #region 未实现log
        public static void LogErrorFormat(string format, params object[] args) { }

        public static void LogException(Exception exception) { }

        public static void LogException(Exception exception, Object context) { }

        public static void LogFormat(string format, params object[] args) { }

        public static void LogFormat(Object context, string format, params object[] args) { }

        public static void LogWarning(object message, Object context) { }

        public static void LogWarningFormat(string format, params object[] args) { }

        public static void LogWarningFormat(Object context, string format, params object[] args) { }
        #endregion
    }
}

