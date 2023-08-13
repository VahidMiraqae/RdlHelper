using System.Collections.Generic;

namespace RdlHelper.ViewModels.RdlCommands
{
    internal abstract class RdlCommand : BaseVm
    {
        protected MainVm _mianVm;
        private bool _showInstructions;

        public RdlCommand(MainVm mainVm)
        {
            _mianVm = mainVm;
        }

        public abstract string Name { get; }
        public abstract string Perform(IEnumerable<string> filePaths);
    }
}