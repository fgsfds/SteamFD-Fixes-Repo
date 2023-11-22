using System.Xml.Serialization;

namespace FixesTests.Helpers
{
    public static class FixesProvider
    {
        public static List<FixesList>? DeserializeFixesXml()
        {
            XmlSerializer xmlSerializer = new(typeof(List<FixesList>));

            var fixesXml = File.ReadAllText("..\\..\\..\\..\\fixes_v3.xml");

            using (TextReader reader = new StringReader(fixesXml))
            {
                return xmlSerializer.Deserialize(reader) as List<FixesList>;
            }
        }
    }
}
