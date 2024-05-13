using System;
using System.Collections.Generic;
using System.Reflection;

namespace iCare.Core.Extensions {
    public static class FieldExtensions {
        public static IEnumerable<FieldInfo> GetFieldsByAttribute<T>(this object targetScript) where T : Attribute {
            if (targetScript == null) yield break;

            const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            var fields = targetScript.GetType().GetFields(FLAGS);

            foreach (var field in fields)
                if (field.GetCustomAttribute<T>() != null)
                    yield return field;
        }

        public static Type GetGenericType(this FieldInfo field) {
            var fieldType = field.FieldType;
            if (fieldType.IsArray)
                return fieldType.GetElementType();

            return fieldType.IsGenericType ? fieldType.GetGenericArguments()[0] : fieldType;
        }
    }
}