﻿using Bannerlord.ButterLib.Common.Extensions;

using MCM.Abstractions;
using MCM.Abstractions.Settings.Base;
using MCM.Abstractions.Settings.Base.PerSave;
using MCM.Abstractions.Settings.Containers;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;

using TaleWorlds.Core;

namespace MCM.Implementation.Settings.Containers.PerSave
{
    internal sealed class MCMPerSaveSettingsContainer : BaseSettingsContainer<PerSaveSettings>, IMCMPerSaveSettingsContainer
    {
        /// <inheritdoc/>
        protected override void RegisterSettings(PerSaveSettings? perSaveSettings)
        {
            var behavior = MCMSubModule.Instance?.GetServiceProvider()?.GetRequiredService<PerSaveCampaignBehavior>();
            if (behavior is null)
                return;

            if (perSaveSettings is null)
                return;

            LoadedSettings.Add(perSaveSettings.Id, perSaveSettings);

            behavior.LoadSettings(perSaveSettings);
        }

        /// <inheritdoc/>
        public override bool SaveSettings(BaseSettings settings)
        {
            var behavior = MCMSubModule.Instance?.GetServiceProvider()?.GetRequiredService<PerSaveCampaignBehavior>();
            if (behavior is null)
                return false;

            if (settings is not PerSaveSettings || !LoadedSettings.ContainsKey(settings.Id))
                return false;

            return behavior.SaveSettings((PerSaveSettings) settings);
        }

        public void OnGameStarted(Game game)
        {
            LoadedSettings.Clear();

            var settings = new List<PerSaveSettings>();
            var allTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) is not null)
                .ToList();

            var mbOptionScreenSettings = allTypes
                .Where(t => typeof(PerSaveSettings).IsAssignableFrom(t))
                .Where(t => !typeof(EmptyPerSaveSettings).IsAssignableFrom(t))
                .Where(t => !typeof(IWrapper).IsAssignableFrom(t))
                .Select(t => Activator.CreateInstance(t) as PerSaveSettings)
                .Where(t => t is not null)
                .Cast<PerSaveSettings>();
            settings.AddRange(mbOptionScreenSettings);

            foreach (var setting in settings)
                RegisterSettings(setting);
        }

        public void OnGameEnded(Game game)
        {
            LoadedSettings.Clear();
        }
    }
}