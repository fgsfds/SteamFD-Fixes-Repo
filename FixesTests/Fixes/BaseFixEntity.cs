﻿using FixesTests.Helpers;
using System.Text.Json.Serialization;

namespace FixesTests.Fixes
{
    /// <summary>
    /// Base fix entity
    /// </summary>
    [JsonDerivedType(typeof(FileFixEntity), typeDiscriminator: "FileFix")]
    [JsonDerivedType(typeof(HostsFixEntity), typeDiscriminator: "HostsFix")]
    [JsonDerivedType(typeof(RegistryFixEntity), typeDiscriminator: "RegistryFix")]
    [JsonDerivedType(typeof(TextFixEntity), typeDiscriminator: "TextFix")]
    public abstract class BaseFixEntity
    {
        /// <summary>
        /// Fix title
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Fix GUID
        /// </summary>
        public required Guid Guid { get; init; }

        /// <summary>
        /// Fix version
        /// </summary>
        public required int Version { get; set; }

        /// <summary>
        /// Supported OSes
        /// </summary>
        public required OSEnum SupportedOSes { get; set; }

        /// <summary>
        /// List of fixes GUIDs that are required for this fix
        /// </summary>
        public List<Guid>? Dependencies { get; set; }

        /// <summary>
        /// Fix description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// List of files that will be backed up before the fix is installed, and the original file will remain
        /// Paths are relative to the game folder, separated by ;
        /// </summary>
        public List<string>? Tags { get; set; }
    }
}