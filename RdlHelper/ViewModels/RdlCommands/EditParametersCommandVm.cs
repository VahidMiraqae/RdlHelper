using RdlHelper.Models;
using RdlHelper.ViewModels.ReportParameterViewModels;
using RdlHelper.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RdlHelper.ViewModels.RdlCommands
{ 
    internal class EditParametersCommandVm : RdlCommandVm
    {
        public EditParametersCommandVm(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Edit\nParameters";

        public override string Perform(IEnumerable<string> filePaths)
        {
            var filePath = filePaths.FirstOrDefault();

            if (filePaths.Count() > 1)
            {
                var fsvm = new FileSelectorVm(filePaths);
                var fsw = new FileSelectorWindow(fsvm);
                fsw.ShowDialog();

                if (fsvm.SelectedItem == null)
                {
                    return "No file selected";
                }

                filePath = fsvm.SelectedItem;
            }


            var doc = new Report(filePath);
              
            
            var vm = new EditParametersVm(doc.Parameters);
            var window = new EditParametersWindow(vm);

            var windowResult = window.ShowDialog();

            //if (windowResult == true)
            //{
            //    doc.ResetParameters(vm.GetParameters());
            //}

            //doc.Overwrite();

            return "Done";
        }
    }
}