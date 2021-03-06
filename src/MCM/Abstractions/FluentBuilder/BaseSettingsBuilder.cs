﻿using Bannerlord.ButterLib.Common.Extensions;

using MCM.Abstractions.Settings.Base.Global;
using MCM.Abstractions.Settings.Base.PerSave;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;

namespace MCM.Abstractions.FluentBuilder
{
    public abstract class BaseSettingsBuilder : ISettingsBuilder
    {
        public static ISettingsBuilder? Create(string id, string displayName) =>
            MCMSubModule.Instance?.GetServiceProvider()?.GetRequiredService<ISettingsBuilderFactory>().Create(id, displayName);

        public abstract ISettingsBuilder SetFolderName(string value);
        public abstract ISettingsBuilder SetSubFolder(string value);
        public abstract ISettingsBuilder SetFormat(string value);
        public abstract ISettingsBuilder SetUIVersion(int value);
        public abstract ISettingsBuilder SetSubGroupDelimiter(char value);
        public abstract ISettingsBuilder SetOnPropertyChanged(PropertyChangedEventHandler value);

        public abstract ISettingsBuilder CreateGroup(string name, Action<ISettingsPropertyGroupBuilder> builder);
        public abstract ISettingsBuilder CreatePreset(string name, Action<ISettingsPresetBuilder> builder);

        public abstract FluentGlobalSettings BuildAsGlobal();
        public abstract FluentPerSaveSettings BuildAsPerSave();
    }
}