using System.Linq;
using System.Reflection;
using iCare.Core.Dependency.Runtime;
using iCare.Core.Dependency.Runtime.Attributes;
using iCare.Core.Dependency.Runtime.Attributes.Get;
using iCare.Core.Extensions;
using UnityEngine;

namespace iCare.Core.Dependency.Editor {
    internal static class InjectorGet {
        internal static void GetInSelf(object targetObj, FieldInfo fieldInfo, BaseDependencyAttribute attribute) {
            if (targetObj is not MonoBehaviour mono) {
                Debug.LogError("Target object is not a MonoBehaviour");
                return;
            }

            if (attribute is not InSelfAttribute) {
                Debug.LogError("Attribute is not InSelf");
                return;
            }

            var result = mono.GetComponent(fieldInfo.GetGenericType());
            fieldInfo.Change(targetObj, result);
        }

        internal static void GetInParent(object targetObj, FieldInfo fieldInfo, BaseDependencyAttribute attribute) {
            if (targetObj is not MonoBehaviour mono) {
                Debug.LogError("Target object is not a MonoBehaviour");
                return;
            }

            if (attribute is not InParentAttribute inParent) {
                Debug.LogError("Attribute is not InParent");
                return;
            }

            var result = mono.GetComponentsInParent(fieldInfo.GetGenericType(), inParent.IncludeInactive);
            if (!inParent.IncludeSelf)
                result = result.Where(x => x.gameObject != mono.gameObject).ToArray();

            fieldInfo.Change(targetObj, result);
        }

        internal static void GetInChildren(object targetObj, FieldInfo fieldInfo, BaseDependencyAttribute attribute) {
            if (targetObj is not MonoBehaviour mono) {
                Debug.LogError("Target object is not a MonoBehaviour");
                return;
            }

            if (attribute is not InChildrenAttribute inChildren) {
                Debug.LogError("Attribute is not InChildren");
                return;
            }

            var result = mono.GetComponentsInChildren(fieldInfo.GetGenericType(), inChildren.IncludeInactive);
            if (!inChildren.IncludeSelf)
                result = result.Where(x => x.gameObject != mono.gameObject).ToArray();

            fieldInfo.Change(targetObj, result);
        }

        internal static void GetInRoot(object targetObj, FieldInfo fieldInfo, BaseDependencyAttribute attribute) {
            if (targetObj is not MonoBehaviour mono) {
                Debug.LogError("Target object is not a MonoBehaviour");
                return;
            }

            if (attribute is not InRootAttribute) {
                Debug.LogError("Attribute is not InRoot");
                return;
            }

            var result = mono.transform.root.GetComponent(fieldInfo.GetGenericType());
            fieldInfo.Change(targetObj, result);
        }
    }
}