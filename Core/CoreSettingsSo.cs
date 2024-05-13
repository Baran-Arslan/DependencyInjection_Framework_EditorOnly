using iCare.Core.Logger;
using iCare.Core.ScriptableSingeleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace iCare.Core {
    [CreateAssetMenu]
    public sealed class CoreSettingsSo : CScriptableSingeleton<CoreSettingsSo> {
        [SerializeField] LogLevel logLevel;
        [SerializeField] [FilePath] string prefabsPath = "Assets/_Project/Prefabs/";
        [SerializeField] [FilePath] string scriptableObjectsPath = "Assets/_Project/Data/";

        public LogLevel LogLevel => logLevel;
        public string PrefabsPath => prefabsPath;
        public string ScriptableObjectsPath => scriptableObjectsPath;
    }
}