using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace iCare.Core.Editor {
    internal static class CAssetUtilities {
        internal static IEnumerable<T> FindAssetsInFolder<T>(string folderPath = "Assets/") where T : Object {
            var useGetComponent = typeof(T).IsSubclassOf(typeof(MonoBehaviour))
                                  || typeof(T) == typeof(MonoBehaviour)
                                  || typeof(T).IsInterface;

            if (useGetComponent) {
                var result = new List<T>();
                var gameObjects = FindAssetsInFolder<GameObject>(folderPath);

                foreach (var foundObj in gameObjects) {
                    var components = foundObj.GetComponentsInChildren<T>();
                    result.AddRange(components);
                }

                return result;
            }

            if (!Directory.Exists(folderPath)) {
                Debug.LogError($"Directory '{folderPath}' does not exist.");
                return Enumerable.Empty<T>();
            }

            var files = Directory.GetFiles(folderPath);
            var assets = files.Select(LoadAssetAtPath<T>).Where(asset => asset != null).ToList();

            var subDirectories = Directory.GetDirectories(folderPath);
            foreach (var subDir in subDirectories) assets.AddRange(FindAssetsInFolder<T>(subDir));

            return assets;
        }

        static T LoadAssetAtPath<T>(string filePath) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(filePath);
        }
    }
}