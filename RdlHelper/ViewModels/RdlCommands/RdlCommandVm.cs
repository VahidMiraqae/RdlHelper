using System;
using System.Collections.Generic;

namespace RdlHelper.ViewModels.RdlCommands
{
    [Obsolete]
    internal abstract class RdlCommandVm : BaseVm
    {
        protected MainVm _mianVm;
        private bool _showInstructions;

        public RdlCommandVm(MainVm mainVm)
        {
            _mianVm = mainVm;
        }

        public abstract string Name { get; }
        public abstract string Perform(IEnumerable<string> filePaths);
    }
}