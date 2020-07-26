﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

using HarmonyLib;

namespace MCM.Custom.Patch.v319
{
    public static class DefaultIngameMenuScreenHandlerPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = instructions.ToList();
            list[23] = new CodeInstruction(OpCodes.Ldc_I4_1);
            return list;
        }
    }
}