using System.IO;
using System.Reflection;

namespace SSFakes.Tests.Utility
{
    public static class PayloadHelper
    {
        public static string LoadTextFromFile(string fileName, string subDirectory = null)
        {
            var appPath = GetAppPath();

            var folderPath = string.IsNullOrWhiteSpace(subDirectory) ? appPath : Path.Combine(appPath, subDirectory);
            var filePath = Path.Combine(folderPath, fileName);

            using TextReader assignmentReader = new StreamReader(filePath);
            return assignmentReader.ReadToEnd();
        }

        public static string GetAppPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var directoryName = Path.GetDirectoryName(codeBase);
            var appPath = directoryName?.Replace(@"file:\", string.Empty);
            // appPath = appPath.Replace(@"\bin\Debug", string.Empty);
            return appPath;
        }
    }
}
