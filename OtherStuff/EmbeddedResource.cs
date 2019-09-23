using System.IO;
using System.Reflection;

namespace DotNet.Cqrs
{
    public static class EmbeddedResource
    {
        public static string ReadSqlFile(string name)
        {
            var assembly = typeof(EmbeddedResource).GetTypeInfo().Assembly;
            var filePath = $"{assembly.FullName.Split(',')[0]}.Queries.{name}.sql";

            string content;
            using (var stream = assembly.GetManifestResourceStream(filePath))
            {
                if (stream == null)
                {
                    throw new System.Exception($"Could not locate embedded resource '{name}', having full name '{filePath}'");
                }

                using (var reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                }
            }

            return content;
        }
    }
}