using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SSFakes.Tests
{
    public static class FakerOutput
    {
        public static void ToJSONFile(object obj, string mocksRelativePath, string fileName)
        {
            var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var directoryName = Path.GetDirectoryName(codeBase);
            var projectBinPath = directoryName?.Replace(@"file:\", string.Empty);

            if (projectBinPath != null)
            {
                var webMocksPath = projectBinPath.Split('\\')
                    .TakeWhile(p => !p.Equals(@"SSFakes.Tests", StringComparison.InvariantCultureIgnoreCase))
                    .Aggregate("", Path.Combine);

                var mockFolderPath = @"PoultryPopulation\FakeData";
                var mocksPath = Path.Combine(Path.Combine(webMocksPath, mockFolderPath), mocksRelativePath);
                if (!Directory.Exists(mocksPath)) {
                    Directory.CreateDirectory(mocksPath);
                }

                var filePath = Path.Combine(mocksPath, $"{Path.GetFileNameWithoutExtension(fileName)}.json");
                using TextWriter textWriterForJsonFile = new StreamWriter(filePath);
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    Converters = new List<JsonConverter> { new StringEnumConverter() }
                };

                var serializer = JsonSerializer.Create(settings);
                serializer.Serialize(textWriterForJsonFile, obj);
            }
        }
    }
}
