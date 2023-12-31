﻿using System.Collections.Generic;

namespace RdlHelper.ViewModels
{
    internal abstract class RdlCommand : BaseVm
    {
        protected MainVm _mianVm;

        public RdlCommand(MainVm mainVm)
        {
            _mianVm = mainVm;
        }

        public abstract string Name { get; }
        public abstract string Perform(IEnumerable<string> filePaths);
    }
}