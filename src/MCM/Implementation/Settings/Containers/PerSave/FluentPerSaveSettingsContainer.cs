﻿using MCM.Abstractions.Settings.Base.PerSave;
using MCM.Abstractions.Settings.Containers;

using TaleWorlds.Core;

namespace MCM.Implementation.Settings.Containers.PerSave
{
    internal sealed class FluentPerSaveSettingsContainer : BaseSettingsContainer<FluentPerSaveSettings>, IMCMFluentPerSaveSettingsContainer
    {
        public void Register(FluentPerSaveSettings settings)
        {
            RegisterSettings(settings);
        }
        public void Unregister(FluentPerSaveSettings settings)
        {
            if (LoadedSettings.ContainsKey(settings.Id))
                LoadedSettings.Remove(settings.Id);
        }

        public void OnGameStarted(Game game) => LoadedSettings.Clear();
        public void OnGameEnded(Game game) => LoadedSettings.Clear();
    }
}