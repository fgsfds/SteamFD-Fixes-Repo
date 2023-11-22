using System.Xml.Serialization;

namespace FixesTests.Helpers
{
    public sealed class FixesList()
    {
        public int GameId { get; set; }

        public string GameName { get; set; }

        [XmlElement("FileFix", typeof(FileFixEntity))]
        public List<object> Fixes { get; set; }
    }

    public sealed partial class FileFixEntity
    {
        public string Name { get; set; }
        public int Version { get; set; }
        public Guid Guid { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public List<string>? Variants { get; set; }
        public string? InstallFolder { get; set; }
        public string? ConfigFile { get; set; }
        public List<string>? FilesToDelete { get; set; }
        public List<string>? FilesToBackup { get; set; }
        public string? RunAfterInstall { get; set; }
        public List<Guid>? Dependencies { get; set; }
        public List<string>? Tags { get; set; }
        public string? MD5 { get; set; }
    }
}
