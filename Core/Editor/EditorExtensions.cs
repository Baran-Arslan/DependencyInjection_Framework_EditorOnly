using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace iCare.Core.Editor {
    internal static class EditorExtensions {
        /// <summary> Marks as addressable and changes name to asset name </summary>
        internal static void MarkAsAddressable(this Object obj, string customName = null) {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group = settings.DefaultGroup; //TODO - Specify group as parameter if needed

            var assetPath = AssetDatabase.GetAssetPath(obj);
            var guid = AssetDatabase.AssetPathToGUID(assetPath);

            var assetName = obj.name;

            if (settings.FindAssetEntry(guid) == null) {
                var entry = settings.CreateOrMoveEntry(guid, group);
                var targetName = string.IsNullOrEmpty(customName) ? assetName : customName;
                entry.address = targetName;
            }

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true);
        }
    }
}