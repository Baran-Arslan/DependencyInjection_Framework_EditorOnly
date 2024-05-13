using iCare.Core.Dependency.Runtime;
using iCare.Core.Editor;
using UnityEditor;

namespace iCare.Core.Dependency.Editor {
    public static class ServiceEnumGenerator {
        [MenuItem("iCare/Generate Service Enums %#T")]
        public static void GenerateServiceEnum() {
            const string PATH = "Assets/Plugins/iCare/CoreExtra/";
            const string TARGET_NAME = "CRef";
            const string SERIALIZED_TARGET_NAME = "CRefSerialized";

            EnumGenerator.GenerateAll<CScriptableService>(PATH, TARGET_NAME);
            EnumGenerator.GenerateAll<CSerializedScriptableService>(PATH, SERIALIZED_TARGET_NAME);
        }
    }
}