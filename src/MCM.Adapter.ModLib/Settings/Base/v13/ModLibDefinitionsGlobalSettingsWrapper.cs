﻿extern alias v13;

using Bannerlord.ButterLib.Common.Helpers;

using HarmonyLib;

using MCM.Abstractions.Settings.Base;
using MCM.Abstractions.Settings.Base.Global;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using LegacyBaseSettings = v13::ModLib.Definitions.SettingsBase;

namespace MCM.Adapter.ModLib.Settings.Base.v13
{
    internal sealed class ModLibDefinitionsGlobalSettingsWrapper : BaseGlobalSettingsWrapper
    {
        private delegate string GetIdDelegate();
        private delegate string GetFolderNameDelegate();
        private delegate string GetDisplayNameDelegate();
        private delegate string GetSubFolderDelegate();

        private readonly GetIdDelegate? _getIdDelegate;
        private readonly GetFolderNameDelegate? _getFolderNameDelegate;
        private readonly GetDisplayNameDelegate? _getDisplayNameDelegate;
        private readonly GetSubFolderDelegate? _getSubFolderDelegate;

        public override string Id => _getIdDelegate?.Invoke() ?? "ERROR";
        public override string FolderName => _getFolderNameDelegate?.Invoke() ?? string.Empty;
        public override string DisplayName => _getDisplayNameDelegate?.Invoke() ?? "ERROR";
        public override int UIVersion => 1;
        public override string SubFolder => _getSubFolderDelegate?.Invoke() ?? string.Empty;
        public override char SubGroupDelimiter => '/';
        public override string FormatType => "memory";
        public override string DiscoveryType => "modlib_v13_attributes";
        public override event PropertyChangedEventHandler? PropertyChanged { add { } remove { } }

        public ModLibDefinitionsGlobalSettingsWrapper(object @object) : base(@object)
        {
            var type = @object.GetType();

            _getIdDelegate = AccessTools2.GetDelegate<GetIdDelegate>(@object, AccessTools.Property(type, nameof(LegacyBaseSettings.ID)).GetMethod);
            _getFolderNameDelegate = AccessTools2.GetDelegate<GetFolderNameDelegate>(@object, AccessTools.Property(type, nameof(LegacyBaseSettings.ModuleFolderName)).GetMethod);
            _getDisplayNameDelegate = AccessTools2.GetDelegate<GetDisplayNameDelegate>(@object, AccessTools.Property(type, nameof(LegacyBaseSettings.ModName)).GetMethod);
            _getSubFolderDelegate = AccessTools2.GetDelegate<GetSubFolderDelegate>(@object, AccessTools.Property(type, nameof(LegacyBaseSettings.SubFolder)).GetMethod);
        }

        internal void UpdateReference(object @object) => Object = @object;

        public override void OnPropertyChanged([CallerMemberName] string? propertyName = null) { }

        protected override BaseSettings CreateNew() => new ModLibDefinitionsGlobalSettingsWrapper(Activator.CreateInstance(Object.GetType())!);
    }
}