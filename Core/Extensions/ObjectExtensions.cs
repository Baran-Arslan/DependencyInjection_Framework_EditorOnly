using UnityObject = UnityEngine.Object;

namespace iCare.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsUnityNull(this object obj)
        {
            return obj == null || ((obj is UnityObject unityObject) && unityObject == null);
        }
    }
}