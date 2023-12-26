using System.Text.Json;

namespace FixesTests.Fixes
{
    public static class FixesProvider
    {
        public static List<FixesList>? DeserializeFixesXml()
        {
            var fixesXml = File.ReadAllText("..\\..\\..\\..\\fixes.json");

            var fixesList = JsonSerializer.Deserialize(fixesXml, FixesListContext.Default.ListFixesList);

            return fixesList;
        }
    }
}
