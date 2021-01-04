using Bannerlord.ButterLib;
using Bannerlord.ButterLib.Common.Helpers;
using Bannerlord.ButterLib.SubModuleWrappers;

using HarmonyLib;

using MCM.Implementation;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace MCM.Tests
{
    public class BaseTests
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static bool MockedGetConfigsPath(ref string __result)
        {
            __result = AppDomain.CurrentDomain.BaseDirectory;
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static bool MockedGetLoadedModules(ref List<ModuleInfo> __result)
        {
            __result = new List<ModuleInfo>();
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static bool MockedGetModulesNames(ref string[] __result)
        {
            __result = new string[0];
            return false;
        }

        private readonly Harmony _harmony = new(nameof(BaseTests));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _harmony.Patch(SymbolExtensions.GetMethodInfo(() => Utilities.GetConfigsPath()),
                prefix: new HarmonyMethod(DelegateHelper.GetMethodInfo(MockedGetConfigsPath)));
            _harmony.Patch(SymbolExtensions.GetMethodInfo(() => ModuleInfoHelper.GetLoadedModules()),
                prefix: new HarmonyMethod(DelegateHelper.GetMethodInfo(MockedGetLoadedModules)));
            _harmony.Patch(SymbolExtensions.GetMethodInfo(() => Utilities.GetModulesNames()),
                prefix: new HarmonyMethod(DelegateHelper.GetMethodInfo(MockedGetModulesNames)));

            var butterLib = new MBSubModuleBaseWrapper(new ButterLibSubModule());
            butterLib.SubModuleLoad();
            new MBSubModuleBaseWrapper(new MCMSubModule()).SubModuleLoad();
            new MBSubModuleBaseWrapper(new MCMImplementationSubModule()).SubModuleLoad();
            butterLib.BeforeInitialModuleScreenSetAsRoot();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _harmony.Unpatch(SymbolExtensions.GetMethodInfo(() => Utilities.GetConfigsPath()),
                DelegateHelper.GetMethodInfo(MockedGetConfigsPath));
            _harmony.Unpatch(SymbolExtensions.GetMethodInfo(() => ModuleInfoHelper.GetLoadedModules()),
                DelegateHelper.GetMethodInfo(MockedGetLoadedModules));
            _harmony.Unpatch(SymbolExtensions.GetMethodInfo(() => Utilities.GetModulesNames()),
                DelegateHelper.GetMethodInfo(MockedGetModulesNames));
        }
    }
}