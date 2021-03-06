﻿extern alias v4;

using v4::MCM.Abstractions.Settings.Definitions;
using v4::MCM.Abstractions.Settings.Definitions.Wrapper;

namespace MCM.Adapter.MCMv3.Settings.Definitions
{
    internal sealed class MCMv3PropertyDefinitionBoolWrapper : BasePropertyDefinitionWrapper,
        IPropertyDefinitionBool
    {
        public MCMv3PropertyDefinitionBoolWrapper(object @object) : base(@object) { }
    }
}