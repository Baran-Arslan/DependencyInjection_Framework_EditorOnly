using System;
using System.Collections;
using System.Collections.Generic;

namespace iCare.Core.Extensions {
    public static class TypeExtensions {
        public static bool IsCollectionType(this object obj) {
            return IsCollectionType(obj?.GetType());
        }

        public static bool IsCollectionType(this Type type) {
            return typeof(ICollection).IsAssignableFrom(type);
        }

        public static bool IsArrayType(this Type type) {
            return type.IsArray;
        }

        public static bool IsHashsetType(this Type type) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>);
        }

        public static bool IsListType(this Type type) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}