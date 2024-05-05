using UnityEngine;

namespace iCare.Core
{
    public static class CLog
    {
        public static void Info(string message, Object obj = null)
        {
            Debug.Log(message, obj);
        }

        public static void Log(string message, Object obj = null)
        {
            Debug.Log(message, obj);
        }

        public static void Warning(string message, Object obj = null)
        {
            Debug.LogWarning(message, obj);
        }

        public static void Error(string message, Object obj = null)
        {
            Debug.LogError(message, obj);
        }
    }
}