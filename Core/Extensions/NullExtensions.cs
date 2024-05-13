using UnityObject = UnityEngine.Object;

namespace iCare.Core.Extensions {
    public static class NullExtensions {
        public static bool IsUnityNull(this object obj) {
            return obj == null || (obj is UnityObject unityObj && unityObj == null);
        }
    }
}