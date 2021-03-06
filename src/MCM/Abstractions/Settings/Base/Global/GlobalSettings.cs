﻿using MCM.Abstractions.Settings.Providers;

using System;
using System.Collections.Concurrent;

namespace MCM.Abstractions.Settings.Base.Global
{
    public abstract class GlobalSettings<T> : GlobalSettings where T : GlobalSettings, new()
    {
        public static T? Instance
        {
            get
            {
                if (!Cache.ContainsKey(typeof(T)))
                    Cache.TryAdd(typeof(T), new T().Id);
                return BaseSettingsProvider.Instance?.GetSettings(Cache[typeof(T)]) as T;
            }
        }
    }

    public abstract class GlobalSettings : BaseSettings
    {
        protected static readonly ConcurrentDictionary<Type, string> Cache = new();
    }
}