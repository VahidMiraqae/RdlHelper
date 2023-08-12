using RdlHelper.Views;
using System.Collections.Generic;
using System.Linq;

namespace RdlHelper.ViewModels
{
    internal class SetDefaultParameteresRdlCommand : RdlCommand
    {
        public SetDefaultParameteresRdlCommand(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Set Default Parameters";

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



            // if 1 Next 
            // if > 1  open New Windows
            // view Name of Rdl Files 
            // user Can Cancel Or chooes Rdl



            throw new System.NotImplementedException();
        }
    }
}