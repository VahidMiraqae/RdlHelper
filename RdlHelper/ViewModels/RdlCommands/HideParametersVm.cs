using RdlHelper.Models;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RdlHelper.ViewModels.RdlCommands
{
    internal class HideParametersVm : RdlCommand
    {
        public HideParametersVm(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Hide\nParameters";

        public override string Perform(IEnumerable<string> filePaths)
        {
            var sb = new StringBuilder();

            foreach (var filePath in filePaths)
            {
                var doc = new RdlXmlDocument(filePath);
                var @params = doc.GetReportParametersElements();

                foreach (var param in @params)
                {
                    param.MakeHidden();
                }

                doc.Overwrite();

                sb.AppendLine($"'{System.IO.Path.GetFileName(filePath)}' => hidden parameters.");
            }
            return sb.ToString();
        }

    }
}