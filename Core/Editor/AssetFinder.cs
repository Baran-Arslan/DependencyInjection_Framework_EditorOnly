using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Core.Editor
{
    internal static class AssetFinder
    {
        public static List<Object> GetEveryObject()
        {
            var allObject = new List<Object>();
            var guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    var scripts = prefab.GetComponentsInChildren<MonoBehaviour>(true);
                    allObject.AddRange(scripts);
                }
            }

            guids = AssetDatabase.FindAssets("t:ScriptableObject");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (scriptableObject != null)
                {
                    allObject.Add(scriptableObject);
                }
            }

            var sceneObjects = Object.FindObjectsOfType<MonoBehaviour>();
            foreach (var mono in sceneObjects)
            {
                allObject.Add(mono);
            }

            return allObject;
        }

        public static List<T> FindByInterface<T>()
        {
            var assetsImplementingInterface = new List<T>();

            var interfaceType = typeof(T);
            if (!interfaceType.IsInterface)
            {
                CLog.Error("Invalid interface type.");
                return assetsImplementingInterface;
            }

            var guids = AssetDatabase.FindAssets("t:Object");

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));

                if (asset != null && AssetImplementsInterface(asset, interfaceType))
                    assetsImplementingInterface.Add((T)(object)asset);
            }

            return assetsImplementingInterface;
        }

        static bool AssetImplementsInterface(Object asset, Type interfaceType)
        {
            var assetType = asset.GetType();
            return assetType.GetInterfaces().Contains(interfaceType);
        }
    }
}