using iCare.Core.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace iCare.Core.ScriptableSingeleton {
    public abstract class CScriptableSingeleton<T> : ScriptableObject, IScriptableSingeleton
        where T : CScriptableSingeleton<T> {
        static T _instance;
        [CRead] [PropertyOrder(-1)] static string AddressableKey => typeof(T).Name;

        public static T Instance {
            get {
                if (_instance != null) return _instance;
                var op = Addressables.LoadAssetAsync<T>(AddressableKey);
                _instance = op.WaitForCompletion();

                return _instance;
            }
        }
    }
}