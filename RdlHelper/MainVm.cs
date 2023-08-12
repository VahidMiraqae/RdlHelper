using System.Collections.Generic;
using System.Windows.Documents;

namespace RdlHelper
{
    internal class MainVm : BaseVm
    {
        private RdlCommand _command;
        private string _message;

        public MainVm()
        {
            RdlCommands.Add(new HideParametersRdlCommand(this));
            RdlCommands.Add(new ShowParametersRdlCommand(this));
        }

        public List<RdlCommand> RdlCommands { get; set; } = new List<RdlCommand>();

        public RdlCommand LastUsedRdlCommand { get => _command; set { _command = value; OnPropChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropChanged(); } }
    }
}