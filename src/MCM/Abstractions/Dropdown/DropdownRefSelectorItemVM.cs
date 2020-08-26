﻿using MCM.Abstractions.Ref;

using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace MCM.Abstractions.Dropdown
{
    internal class DropdownRefSelectorItemVM : DropdownSelectorItemVMBase
    {
        private readonly IRef _ref;

        [DataSourceProperty]
        public override string StringItem => _ref.Value.ToString();

        [DataSourceProperty]
        public override HintViewModel Hint { get; } = new HintViewModel("");

        public DropdownRefSelectorItemVM(IRef @ref)
        {
            _ref = @ref;
        }

        public override string ToString() => _ref.Value.ToString();
    }
}