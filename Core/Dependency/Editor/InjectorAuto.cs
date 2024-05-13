using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using iCare.Core.Dependency.Runtime;
using iCare.Core.Dependency.Runtime.Attributes;
using iCare.Core.Dependency.Runtime.Attributes.Auto;
using iCare.Core.Dependency.Runtime.Interfaces;
using iCare.Core.Extensions;
using iCare.Core.Logger;
using Sirenix.Utilities;

namespace iCare.Core.Dependency.Editor {
    internal static class InjectorAuto {
        static HashSet<IService> _services => DependencyInjector.AllServices;

        internal static void InjectAuto(object targetObj, FieldInfo fieldInfo, BaseDependencyAttribute attribute) {
            if (attribute is not AutoAttribute autoAttribute) {
                CLog.Error("Trying to inject Auto without AutoAttribute.");
                return;
            }

            var data = GetForAuto(fieldInfo.GetGenericType(), autoAttribute.GetId());
            fieldInfo.Change(targetObj, data);
        }

        internal static void InjectAutoList(object targetObj, FieldInfo propertyInfo,
            BaseDependencyAttribute attribute) {
            if (attribute is not AutoListAttribute autoAllAttribute) {
                CLog.Error("Trying to inject AutoAll without AutoAllAttribute.");
                return;
            }

            var data = GetForAutoAll(propertyInfo.GetGenericType(), autoAllAttribute.GetId());
            propertyInfo.Change(targetObj, data);
        }

        static object GetForAuto(Type targetType, string targetID = null) {
            var result = Enumerable.ToList(_services.Where(service => service.ServiceTypes
                    .Contains(targetType) && CheckServiceID(targetID, service.ID))
                .Select(service => service.Service));

            if (result.IsNullOrEmpty()) {
                CLog.Error("No service found for type <color=red>" + targetType + "</color> and ID <color=red>" +
                           targetID + ". </color>");
                return null;
            }

            if (result.Count > 1)
                CLog.Error("Multiple service found for <color=red>" + targetType + "</color> and ID <color=red>" +
                           targetID + ". </color>");


            var data = result.FirstOrDefault();
            if (data.IsUnityNull())
                CLog.Error("No data found for type <color=red>" + targetType + "</color> and ID <color=red>" +
                           targetID + ". </color>");
            return data;
        }

        static IEnumerable<object> GetForAutoAll(Type type, string targetID = null) {
            List<object> result;
            if (string.IsNullOrEmpty(targetID))
                result = Enumerable.ToList(_services.Where(service => service.ServiceTypes
                        .Contains(type))
                    .Select(service => service.Service));
            else
                result = Enumerable.ToList(_services.Where(service => service.ServiceTypes
                        .Contains(type) && service.ID == targetID)
                    .Select(service => service.Service));

            foreach (var data in Enumerable.ToList(result).Where(data => data.IsUnityNull())) result.Remove(data);


            return result;
        }

        static bool CheckServiceID(string targetID, string myID) {
            return targetID == myID || (string.IsNullOrEmpty(targetID) && string.IsNullOrEmpty(myID));
        }
    }
}