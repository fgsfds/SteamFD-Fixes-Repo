using System.Collections.ObjectModel;

namespace FixesTests.Helpers
{
    /// <summary>
    /// Entity containing game information and a list of fixes for it
    /// </summary>
    public sealed class FixesList
    {
        /// <summary>
        /// Steam ID of the game
        /// </summary>
        public int GameId { get; init; }

        /// <summary>
        /// Game title
        /// </summary>
        public string GameName { get; init; }

        /// <summary>
        /// List of fixes 
        /// </summary>
        public ObservableCollection<FixEntity> Fixes { get; init; }

        public FixesList(
            int gameId,
            string gameName,
            ObservableCollection<FixEntity> fixes
            )
        {
            GameId = gameId;
            GameName = gameName;
            Fixes = fixes;
        }

        /// <summary>
        /// Serializer constructor
        /// </summary>
        private FixesList()
        {
        }
    }

    /// <summary>
    /// Fix entity
    /// </summary>
    public sealed partial class FixEntity
    {
        /// <summary>
        /// Fix title
        /// </summary>
        public string Name;

        /// <summary>
        /// Fix version
        /// </summary>
        public int Version;

        /// <summary>
        /// Fix GUID
        /// </summary>
        public Guid Guid { get; init; }

        /// <summary>
        /// Download URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Fix description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of fix's variants
        /// Names of folders in inside a fix's archive, separated by ;
        /// </summary>
        public List<string>? Variants { get; set; }

        /// <summary>
        /// Folder to unpack ZIP
        /// Relative to the game folder
        /// </summary>
        public string? InstallFolder { get; set; }

        /// <summary>
        /// Fix configuration file
        /// Can be any file including .exe
        /// Path is relative to the game folder
        /// </summary>
        public string? ConfigFile { get; set; }

        /// <summary>
        /// List of files that will be backed up and deleted before the fix is installed
        /// Paths are relative to the game folder, separated by ;
        /// </summary>
        public string? FilesToDelete { get; set; }

        /// <summary>
        /// List of files that will be backed up before the fix is installed, and the original file will remain
        /// Paths are relative to the game folder, separated by ;
        /// </summary>
        public string? FilesToBackup { get; set; }

        /// <summary>
        /// File that will be run after the fix is installed
        /// Path is relative to the game folder
        /// </summary>
        public string? RunAfterInstall { get; set; }

        /// <summary>
        /// List of fixes GUIDs that are required for this fix
        /// </summary>
        public List<Guid> Dependencies { get; set; }
    }
}
