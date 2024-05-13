using System.Linq;
using iCare.Core.Editor;
using UnityEditor;
using UnityEngine;
using AssetUtilities = Sirenix.Utilities.Editor.AssetUtilities;

namespace iCare.Core.ScriptableSingeleton.Editor {
    public sealed class ScriptableSingletonEditor : AssetPostprocessor {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths) {
            foreach (var asset in importedAssets) {
                if (!asset.EndsWith(".asset")) continue;


                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(asset);
                if (obj == null) continue;

                if (obj is IScriptableSingeleton singeleton) {
                    var allAssets = AssetUtilities.GetAllAssetsOfType(singeleton.GetType());
                    var foundCount = allAssets.Count();

                    if (foundCount > 1) {
                        Debug.LogError("Cant create a second singeleton deleting new one");
                        AssetDatabase.DeleteAsset(asset);
                        AssetDatabase.Refresh();
                    }
                    else {
                        obj.MarkAsAddressable(obj.GetType().Name);
                    }
                }
            }
        }
    }
}