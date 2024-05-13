using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Core.Logger {
    public static class CLog {
        static LogLevel _logLevel => CoreSettingsSo.Instance.LogLevel;

        public static void Info(string message, Object user = null) {
            if (_logLevel <= LogLevel.Info)
                Debug.Log($"<color=green>[{DateTime.Now:HH:mm:ss}] [INFO]</color> {message}", user);
        }

        public static void Log(string message, Object user = null) {
            if (_logLevel <= LogLevel.Log)
                Debug.Log($"<color=white>[{DateTime.Now:HH:mm:ss}] [LOG]</color> {message}", user);
        }

        public static void Warning(string message, Object user = null) {
            if (_logLevel <= LogLevel.Warning)
                Debug.LogWarning($"<color=yellow>[{DateTime.Now:HH:mm:ss}] [WARNING]</color> {message}", user);
        }

        public static void Error(string message, Object user = null) {
            if (_logLevel <= LogLevel.Error)
                Debug.LogError($"<color=red>[{DateTime.Now:HH:mm:ss}] [ERROR]</color> {message}", user);
        }
    }
}