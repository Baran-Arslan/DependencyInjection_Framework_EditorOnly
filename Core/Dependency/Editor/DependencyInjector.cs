using System.Collections.Generic;
using System.Linq;
using iCare.Core.Dependency.Runtime;
using iCare.Core.Dependency.Runtime.Attributes;
using iCare.Core.Dependency.Runtime.Attributes.Auto;
using iCare.Core.Dependency.Runtime.Attributes.Get;
using iCare.Core.Dependency.Runtime.Interfaces;
using iCare.Core.Editor;
using iCare.Core.Extensions;
using iCare.Core.Logger;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Core.Dependency.Editor {
    internal static class DependencyInjector {
        internal static readonly HashSet<IService> AllServices = new();

        [MenuItem("iCare/Inject All %#E")]
        internal static void InjectAll() {
            var allObjects = new List<object>();
            var prefabsPath = CoreSettingsSo.Instance.PrefabsPath;
            var scriptableObjectsPath = CoreSettingsSo.Instance.ScriptableObjectsPath;

            allObjects.AddRange(Object.FindObjectsOfType<MonoBehaviour>());
            allObjects.AddRange(CAssetUtilities.FindAssetsInFolder<MonoBehaviour>(prefabsPath));
            allObjects.AddRange(CAssetUtilities.FindAssetsInFolder<MonoBehaviour>("Assets/Resources/"));
            allObjects.AddRange(CAssetUtilities.FindAssetsInFolder<ScriptableObject>("Assets/Resources/"));


            var allScriptableObjects = CAssetUtilities.FindAssetsInFolder<ScriptableObject>(scriptableObjectsPath);
            foreach (var scriptableObject in allScriptableObjects) {
                allObjects.Add(scriptableObject);

                var interfaceType = typeof(IService);
                var scriptableType = scriptableObject.GetType();
                if (interfaceType.IsAssignableFrom(scriptableType)) AllServices.Add(scriptableObject as IService);
            }

            CLog.Log("Total found service count: " + AllServices.Count);

            foreach (var obj in allObjects) Inject(obj);
        }

        static void Inject(object targetObj) {
            var dependencyFields = targetObj.GetFieldsByAttribute<BaseDependencyAttribute>().ToArray();
            if (dependencyFields.IsNullOrEmpty())
                return;

            CLog.Info("Injecting dependencies for: " + targetObj + " with " + dependencyFields.Length + " fields.");


            foreach (var fieldInfo in dependencyFields) {
                var attribute = fieldInfo.GetAttribute<BaseDependencyAttribute>();
                switch (attribute.GetType()) {
                    case var type when type == typeof(InSelfAttribute):
                        InjectorGet.GetInSelf(targetObj, fieldInfo, attribute);
                        break;
                    case var type when type == typeof(InParentAttribute):
                        InjectorGet.GetInParent(targetObj, fieldInfo, attribute);
                        break;
                    case var type when type == typeof(InChildrenAttribute):
                        InjectorGet.GetInChildren(targetObj, fieldInfo, attribute);
                        break;
                    case var type when type == typeof(InRootAttribute):
                        InjectorGet.GetInRoot(targetObj, fieldInfo, attribute);
                        break;
                    case var type when type == typeof(AutoAttribute):
                        InjectorAuto.InjectAuto(targetObj, fieldInfo, attribute);
                        break;
                    case var type when type == typeof(AutoListAttribute):
                        InjectorAuto.InjectAutoList(targetObj, fieldInfo, attribute);
                        break;
                    default:
                        Debug.LogError("Unknown attribute type: " + attribute.GetType());
                        return;
                }
            }
        }
    }
}