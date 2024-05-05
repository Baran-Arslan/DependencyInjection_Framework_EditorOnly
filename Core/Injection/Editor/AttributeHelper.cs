using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace iCare.Core.Injection.Editor
{
    internal static class AttributeHelper
    {
        const BindingFlags BINDING_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static IEnumerable<FieldInfo> GetFields<T>(object targetObject, BindingFlags bindingFlags = default)
            where T : Attribute
        {
            Debug.Assert(targetObject != null, "Target object is null");
            if (bindingFlags == default) bindingFlags = BINDING_FLAGS;

            var allFields = targetObject.GetType().GetFields(bindingFlags);
            return allFields.Where(field => field.GetCustomAttribute(typeof(T)) != null).ToList();
        }
    }
}