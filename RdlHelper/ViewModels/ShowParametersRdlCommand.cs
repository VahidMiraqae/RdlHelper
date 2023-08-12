using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RdlHelper.ViewModels
{
    internal class ShowParametersRdlCommand : RdlCommand
    {
        public ShowParametersRdlCommand(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Show Parameters";


        public override string Perform(IEnumerable<string> filePaths)
        {
            var sb = new StringBuilder();

            foreach (var filePath in filePaths)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                var parametersNode = xmlDoc.GetElementsByTagName("ReportParameters");

                if (parametersNode.Count != 1)
                {
                    return "No Report Parameters";
                }

                var a = (XmlElement)parametersNode[0];

                for (int i = 0; i < a.ChildNodes.Count; i++)
                {
                    var b = (XmlElement)a.ChildNodes[i];
                    var hiddenNode = b.GetElementsByTagName("Hidden");

                    if (hiddenNode.Count != 0)
                    {
                        b.RemoveChild(hiddenNode[0]);
                    }

                }

                xmlDoc.Save(filePath);
                sb.AppendLine($"'{System.IO.Path.GetFileName(filePath)}' => visible parameters.");
            }

            return sb.ToString();
        }
    }
}