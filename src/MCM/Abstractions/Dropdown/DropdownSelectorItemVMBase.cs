﻿using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace MCM.Abstractions.Dropdown
{
    internal abstract class DropdownSelectorItemVMBase : ViewModel
    {
        [DataSourceProperty]
        public bool CanBeSelected { get; set; } = true;

        [DataSourceProperty]
        public abstract string StringItem { get; }

        [DataSourceProperty]
        public abstract HintViewModel Hint { get; }
    }
}