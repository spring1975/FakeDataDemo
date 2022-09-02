using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pluralize.NET.Core;
using SSFakes.Tests.Utility;

namespace Utilities
{
    public static class PayloadJsonSerializationHelpers
    {
        public static string ToJSON<T>(T obj)
        {
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            using var stringWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(stringWriter) {Formatting = Formatting.Indented};
            serializer.Serialize(jsonWriter, obj);
            return stringWriter.ToString();
        }

        public static string ToJSONFile(object obj, string subDirectory = null)
        {
            var className = GetClassName(obj);
            var appPath = PayloadHelper.GetAppPath();

            var folderPath = string.IsNullOrWhiteSpace(subDirectory) ? appPath : Path.Combine(appPath, subDirectory);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, $"{className}.json");


            using StreamWriter file = File.CreateText(filePath);
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            serializer.Serialize(file, obj);

            return filePath;
        }

        private static string GetClassName(object obj)
        {
            Type type = obj.GetType();
            if (!type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(IList<>)
                )
            ) return type.Name;

            var singularName = type.GetGenericArguments().FirstOrDefault()?.Name;
            var outputName = string.IsNullOrWhiteSpace(singularName)
                ? "UhWhaaaa"
                : new Pluralizer().Pluralize(singularName);
            return $"ListOf{outputName}";
        }

        public static T FromJson<T>(string filePath)
        {

            var appPath = PayloadHelper.GetAppPath();

            var jsonString = filePath.Contains(appPath)
                ? PayloadHelper.LoadTextFromFile(filePath)
                : PayloadHelper.LoadTextFromFile(Path.Combine(appPath, filePath));

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
