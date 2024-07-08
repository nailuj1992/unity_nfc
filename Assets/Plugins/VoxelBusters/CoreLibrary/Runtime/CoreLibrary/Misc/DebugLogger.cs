using UnityEngine;

using Exception = System.Exception;

namespace VoxelBusters.CoreLibrary
{
    public static class DebugLogger 
    {
        #region Constants

        private const string kDefaultTag = "VoxelBusters";

        #endregion

        #region Static fields

        private static LogLevel s_logLevel;

        #endregion

        #region Static methods

        public static void SetLogLevel(LogLevel value)
        {
            s_logLevel  = value;
        }

        #endregion

        #region Log methods

        public static void Log(string message, Object context = null)
        {
            Log(kDefaultTag, message, context);
        }

        public static void Log(string tag, string message, Object context = null)
        {
            // Check whether the specified request is allowed
            if (IgnoreLog(LogLevel.Info)) return;

            Debug.Log($"[{tag}] {message}", context);
        }

        public static void LogWarning(string message, Object context = null)
        {
            LogWarning(kDefaultTag, message, context);
        }

        public static void LogWarning(string tag, string message, Object context = null)
        {
            // Check whether the specified request is allowed
            if (IgnoreLog(LogLevel.Warning)) return;

            Debug.LogWarning($"[{tag}] {message}", context);
        }

        public static void LogError(string message, Object context = null)
        {
            LogError(kDefaultTag, message, context);
        }

        public static void LogError(string tag, string message, Object context = null)
        {
            // Check whether the specified request is allowed
            if (IgnoreLog(LogLevel.Error)) return;

            Debug.LogError($"[{tag}] {message}", context);
        }

        public static void LogException(Exception exception, Object context = null)
        {
            LogException(kDefaultTag, exception, context);
        }

        public static void LogException(string tag, Exception exception, Object context = null)
        {
            // Check whether the specified request is allowed
            if (IgnoreLog(LogLevel.Critical)) return;

            Debug.LogError($"[{tag}] {exception}", context);
        }

        #endregion

        #region Obsolete methods

        [System.Obsolete("This method is deprecated. Use Log instead.", false)]
        public static void LogFormat(string format, params object[] arguments)
        {
            LogFormat(null, format, kDefaultTag, arguments);
        }

        [System.Obsolete("This method is deprecated. Use Log instead.", false)]
        public static void LogFormat(Object context, string format, params object[] arguments)
        {
            // Check whether logging is required
            if (IgnoreLog(LogLevel.Info)) return;

            var     formattedMessage    = $"[VoxelBusters] {string.Format(format, arguments)}";
            Debug.Log(formattedMessage, context);
        }

        [System.Obsolete("This method is deprecated. Use LogWarning instead.", false)]
        public static void LogWarningFormat(string format, params object[] arguments)
        {
            LogWarningFormat(null, format, arguments);
        }

        [System.Obsolete("This method is deprecated. Use LogWarning instead.", false)]
        public static void LogWarningFormat(Object context, string format, params object[] arguments)
        {
            // Check whether logging is required
            if (IgnoreLog(LogLevel.Warning)) return;

            var     formattedMessage    = "[VoxelBusters] " + string.Format(format, arguments);
            Debug.LogWarning(formattedMessage, context);
        }

        [System.Obsolete("This method is deprecated. Use LogError instead.", false)]
        public static void LogErrorFormat(string format, params object[] arguments)
        {
            LogErrorFormat(null, format, arguments);
        }

        [System.Obsolete("This method is deprecated. Use LogError instead.", false)]
        public static void LogErrorFormat(Object context, string format, params object[] arguments)
        {
            // Check whether logging is required
            if (IgnoreLog(LogLevel.Error)) return;

            var     formattedMessage    = "[VoxelBusters] " + string.Format(format, arguments);
            Debug.LogError(formattedMessage, context);
        }

        #endregion

        #region Private static methods

        private static bool IgnoreLog(LogLevel level)
        {
            return (level < s_logLevel);
        }

        #endregion

        #region Nested types

        public enum LogLevel
        {            
            Info = 0,

            Warning,

            Error,

            Critical,

            None
        }

        #endregion
    }
}