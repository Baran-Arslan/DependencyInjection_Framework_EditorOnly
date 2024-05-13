using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using iCare.Core.Extensions;
using UnityEngine;

namespace iCare.Core.Dependency.Editor {
    public static class FieldChanger {
        public static void Change(this FieldInfo fieldInfo, object fieldObj, object data) {
            if (TryToSetField(fieldInfo, fieldObj, data)) return;

            var dataIsEnumerable = data.IsCollectionType();
            var fieldIsEnumerable = fieldInfo.FieldType.IsCollectionType();


            if (data is not IEnumerable dataAsEnumerable) {
                Debug.LogError("Data is not enumerable" + data);
                return;
            }

            var castedData = dataAsEnumerable.Cast<object>();

            if (!fieldIsEnumerable && dataIsEnumerable) {
                Debug.Log("Data is enumerable but field is not enumerable, setting first element of data to field");
                TryToSetField(fieldInfo, fieldObj, castedData.FirstOrDefault(), true);
                return;
            }

            if (fieldInfo.FieldType.IsListType())
                SetList(fieldInfo, fieldObj, castedData);
            else if (fieldInfo.FieldType.IsHashsetType())
                SetHashSet(fieldInfo, fieldObj, castedData);
            else if (fieldInfo.FieldType.IsArrayType())
                SetArray(fieldInfo, fieldObj, castedData);
            else
                Debug.LogError("Field type is not supported");
        }

        static void SetHashSet(FieldInfo field, object fieldObject, IEnumerable<object> data) {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetGenericArguments()[0];
            var hashSet = Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(elementType));

            var addMethod = fieldType.GetMethod("Add");
            foreach (var item in data) {
                var convertedValue = elementType.IsInstanceOfType(item) ? item : Convert.ChangeType(item, elementType);

                if (addMethod != null) addMethod.Invoke(hashSet, new[] { convertedValue });
            }

            field.SetValue(fieldObject, hashSet);
        }

        static void SetList(FieldInfo field, object fieldObject, IEnumerable<object> data) {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetGenericArguments()[0];
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (var item in data) {
                var convertedValue = elementType.IsInstanceOfType(item) ? item : Convert.ChangeType(item, elementType);

                list.Add(convertedValue);
            }

            field.SetValue(fieldObject, list);
        }

        static void SetArray(FieldInfo field, object fieldObject, IEnumerable<object> data) {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetElementType();
            if (elementType == null) return;
            data = data.ToArray();
            var convertedArray = Array.CreateInstance(elementType, data.Count());

            var i = 0;
            foreach (var item in data) {
                var convertedValue =
                    elementType.IsInstanceOfType(item) ? item : Convert.ChangeType(item, elementType);

                convertedArray.SetValue(convertedValue, i);
                i++;
            }

            field.SetValue(fieldObject, convertedArray);
        }

        static bool TryToSetField(FieldInfo fieldInfo, object fieldObj, object data, bool log = false) {
            if (fieldInfo.FieldType.IsInstanceOfType(data)) {
                fieldInfo.SetValue(fieldObj, data);
                return true;
            }

            if (log)
                Debug.LogError("Data type is not assignable to field type");
            return false;
        }
    }
}