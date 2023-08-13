using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RdlHelper.ViewModels.RdlCommands;

namespace RdlHelper.ViewModels
{
    internal class MainVm : BaseVm
    {
        private RdlCommand _command;
        private string _message;

        public MainVm()
        {
            //RdlCommands.Add(new HideParametersVm(this));
            //RdlCommands.Add(new ShowParametersVm(this));
            //RdlCommands.Add(new SetDefaultParameteresVm(this));
             

            // this is the reflection approach

            var thisAssembly = Assembly.GetAssembly(GetType());
            var types = thisAssembly.GetTypes().Where(aa => aa.IsSubclassOf(typeof(RdlCommand)))
                .Where(aa => aa.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Select(aa => (RdlCommand)Activator.CreateInstance(aa, this));

            RdlCommands = new List<RdlCommand>(types);
        }

        public List<RdlCommand> RdlCommands { get; set; }

        public RdlCommand LastUsedRdlCommand { get => _command; set { _command = value; OnPropChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropChanged(); } }
    }
}