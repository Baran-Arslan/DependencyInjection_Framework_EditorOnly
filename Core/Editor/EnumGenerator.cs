using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace iCare.Core.Editor {
    internal static class EnumGenerator {
        internal static IReadOnlyList<T> GenerateAll<T>(string enumCreatePath, string targetEnumName) where T : Object {
            var allAssets = AssetUtilities.GetAllAssetsOfType<T>().ToArray();
            var enumsToBeAdded = allAssets.Select(asset => asset.name).ToList();
            Generate(enumCreatePath, targetEnumName, enumsToBeAdded);
            return allAssets;
        }

        static void Generate(string enumCreatePath, string targetEnumName, IReadOnlyList<string> enumsToBeAdded) {
            var filePathAndName = GetPath(enumCreatePath, targetEnumName);
            var oldEnums = GetCurrentEnums(filePathAndName);

            var highestValue = 1;
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
            streamWriter.WriteLine("    Empty = 0,");

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