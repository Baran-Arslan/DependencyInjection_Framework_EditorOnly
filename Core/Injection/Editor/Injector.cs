using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using iCare.Core.Editor;
using iCare.Core.Extensions;
using iCare.Core.Injection.Attributes;
using UnityEngine;

namespace iCare.Core.Injection.Editor
{
    internal static class Injector
    {
        public static Action OnInject;

        public static void InjectEverything()
        {
            foreach (var obj in AssetFinder.GetEveryObject())
            {
                Inject(obj);
            }

            OnInject?.Invoke();
        }

        static void Inject(object targetObject)
        {
            var fields = AttributeHelper.GetFields<AutoAttribute>(targetObject);
            foreach (var field in fields)
            {
                Debug.Log(targetObject.GetType());

                field.SetValue(targetObject, null);
                var autoAttribute = field.GetCustomAttribute<AutoAttribute>();
                var serviceID = autoAttribute.ID;
                var fieldType = field.FieldType;


                var allAssets = AssetFinder.FindByInterface<IService>();
                FixAssets(ref allAssets, fieldType, serviceID);

                if (allAssets.Count > 1)
                    CLog.Error("Found multiple assets with ID<color=red> " + serviceID +
                               "</color> for type <color=red>" + allAssets[0].GetType() + "</color>");
                else if (allAssets.Count == 0)
                    CLog.Error("No asset found with ID<color=red> " + serviceID + "</color> for type <color=red>" +
                               fieldType + "</color>");
                else
                {
                    var asset = allAssets[0];
                    if (fieldType.IsInstanceOfType(asset.ServiceData))
                        field.SetValue(targetObject, asset.ServiceData);
                    else
                        Debug.LogError("Asset " + asset.GetType() + " with ID " + asset.GetServiceID() +
                                       " does not match the type of " + fieldType);
                }
            }
        }

        static void FixAssets(ref List<IService> foundAssets, Type fieldType, string serviceID)
        {
            foreach (var service in foundAssets.ToList())
            {
                if (service.ServiceData.IsUnityNull())
                {
                    CLog.Error(service.GetType() + " with ID " + service.GetServiceID() + " has null data.");
                    foundAssets.Remove(service);
                    continue;
                }

                if (service.GetServiceID() != serviceID)
                {
                    foundAssets.Remove(service);
                    continue;
                }

                var isMatch = service.RegisterTypes.Any(type => type == fieldType);
                if (isMatch) continue;
                foundAssets.Remove(service);
            }
        }
    }
}