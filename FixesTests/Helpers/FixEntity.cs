namespace FixesTests.Helpers
{
    public sealed class FixesList
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public List<FixEntity> Fixes { get; set; }
        private FixesList()
        {
        }
    }

    public sealed partial class FixEntity
    {
        public string Name;
        public int Version;
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
    }
}
