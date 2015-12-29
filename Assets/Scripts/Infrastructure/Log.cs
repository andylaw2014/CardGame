using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Infrastructure
{
    public static class Log
    {
        #region Error

        public static void ErrorFormat(Object context, string template, params object[] args)
        {
            var message = string.Format(template, args);
            Error(context, message);
        }

        public static void ErrorFormat(string template, params object[] args)
        {
            var message = string.Format(template, args);
            Error(message);
        }

        public static void Error(object message)
        {
            Debug.LogError(message);
        }

        public static void Error(Object context, object message)
        {
            Debug.LogError(message, context);
        }

        #endregion

        #region Warning

        public static void WarningFormat(Object context, string template, params object[] args)
        {
            var message = string.Format(template, args);
            Warning(context, message);
        }

        public static void WarningFormat(string template, params object[] args)
        {
            var message = string.Format(template, args);
            Warning(message);
        }

        public static void Warning(object message)
        {
            Debug.LogWarning(message);
        }

        public static void Warning(Object context, object message)
        {
            Debug.LogWarning(message, context);
        }

        #endregion

        #region Message

        public static void MessageFormat(Object context, string template, params object[] args)
        {
            var message = string.Format(template, args);
            Message(context, message);
        }

        public static void MessageFormat(string template, params object[] args)
        {
            var message = string.Format(template, args);
            Message(message);
        }

        public static void Message(object message)
        {
            Debug.Log(message);
        }

        public static void Message(Object context, object message)
        {
            Debug.Log(message, context);
        }

        #endregion

        #region Verbose

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void VerboseFormat(Object context, string template, params object[] args)
        {
            var message = string.Format(template, args);
            Verbose(context, message);
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void VerboseFormat(string template, params object[] args)
        {
            var message = string.Format(template, args);
            Verbose(message);
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Verbose(object message)
        {
            Debug.Log(string.Concat("<color=grey>[VERBOSE]</color> ", message));
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Verbose(Object context, object message)
        {
            Debug.Log(string.Concat("<color=grey>[VERBOSE]</color> ", message), context);
        }

        #endregion
    }
}