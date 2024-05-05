using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace iCare.Core.Editor {
    public static class EnumGenerator {
        public static IReadOnlyList<T> GenerateAll<T>(string enumCreatePath, string targetEnumName) where T : Object {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            var enumsToBeAdded = new string[guids.Length];
            var assets = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++) {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                assets[i] = asset;
                enumsToBeAdded[i] = asset.name;
            }

            Generate(enumCreatePath, targetEnumName, enumsToBeAdded);
            return assets;
        }

        static void Generate(string enumCreatePath, string targetEnumName, IReadOnlyList<string> enumsToBeAdded) {
            var filePathAndName = GetPath(enumCreatePath, targetEnumName);
            var oldEnums = GetCurrentEnums(filePathAndName);

            var highestValue = 0;
            if (oldEnums != null && oldEnums.Any())
                highestValue = oldEnums.Values.Max() + 1;

            WriteEnums(targetEnumName, enumsToBeAdded, filePathAndName, oldEnums, highestValue);

            AssetDatabase.Refresh();
        }

        static void WriteEnums(string targetEnumName, IReadOnlyList<string> enumsToBeAdded, string filePathAndName,
            IReadOnlyDictionary<string, int> oldEnums, int highestValue) {
            using var streamWriter = new StreamWriter(filePathAndName);
            streamWriter.WriteLine("public enum " + targetEnumName);
            streamWriter.WriteLine("{");
            streamWriter.WriteLine("    Empty = -1,");

            for (var index = 0; index < enumsToBeAdded.Count; index++) {
                var enumString = enumsToBeAdded[index];
                if (oldEnums != null && oldEnums.TryGetValue(enumString, out var oldEnumNumber)) {
                    streamWriter.WriteLine("    " + enumString + " = " + oldEnumNumber + ",");
                    continue;
                }

                var newEnumNumber = index + highestValue;
                streamWriter.WriteLine("    " + enumString + " = " + newEnumNumber + ",");
            }

            streamWriter.WriteLine("}");
        }

        static string GetPath(string enumCreatePath, string targetEnumName) {
            var filePathAndName = enumCreatePath + targetEnumName + ".cs";
            if (!Directory.Exists(enumCreatePath)) Directory.CreateDirectory(enumCreatePath);
            return filePathAndName;
        }

        static Dictionary<string, int> GetCurrentEnums(string filePathAndName) {
            if (!File.Exists(filePathAndName)) return null;

            var oldEnums = new Dictionary<string, int>();

            var lines = File.ReadAllLines(filePathAndName);
            foreach (var line in lines) {
                if (!line.Contains("=")) continue;

                var enumName = line.Split('=')[0].Trim();
                var enumNumber = line.Split('=')[1].Trim().TrimEnd(',');
                oldEnums.Add(enumName, int.Parse(enumNumber));
            }

            return oldEnums;
        }
    }
}