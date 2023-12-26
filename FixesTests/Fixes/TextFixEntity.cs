﻿using FixesTests.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace FixesTests.Fixes
{
    public sealed class TextFixEntity : BaseFixEntity
    {
        [SetsRequiredMembers]
        public TextFixEntity()
        {
            Name = string.Empty;
            Version = 1;
            Guid = Guid.NewGuid();
            Description = null;
            Dependencies = null;
            Tags = null;
            SupportedOSes = OSEnum.Windows;
        }

        [SetsRequiredMembers]
        public TextFixEntity(BaseFixEntity fix)
        {
            Name = fix.Name;
            Version = fix.Version;
            Guid = fix.Guid;
            Description = fix.Description;
            Dependencies = null;
            Tags = fix.Tags;
            SupportedOSes = OSEnum.Windows;
        }
    }
}
