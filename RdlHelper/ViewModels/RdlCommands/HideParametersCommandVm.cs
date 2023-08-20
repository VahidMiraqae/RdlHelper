using RdlHelper.Models;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RdlHelper.ViewModels.RdlCommands
{
    internal class HideParametersCommandVm : RdlCommand
    {
        public HideParametersCommandVm(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Hide\nParameters";

        public override string Perform(IEnumerable<string> filePaths)
        {
            var sb = new StringBuilder();

            foreach (var filePath in filePaths)
            {
                var doc = new RdlDocument(filePath);
                var @params = doc.GetParameters();

                foreach (var param in @params)
                {
                    param.Hidden = true;
                }

                doc.Overwrite();

                sb.AppendLine($"'{System.IO.Path.GetFileName(filePath)}' => hidden parameters.");
            }
            return sb.ToString();
        }

    }
}