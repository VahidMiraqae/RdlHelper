using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RdlHelper.ViewModels
{
    internal class HideParametersRdlCommand : RdlCommand
    {
        public HideParametersRdlCommand(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Hide Parameters";

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

                    if (hiddenNode.Count == 0)
                    {
                        var xmlEl = xmlDoc.CreateElement("Hidden", b.NamespaceURI);
                        xmlEl.InnerText = "true";

                        b.AppendChild(xmlEl);
                    }

                }

                xmlDoc.Save(filePath);
                sb.AppendLine($"'{System.IO.Path.GetFileName(filePath)}' => hidden parameters.");
            }
            return sb.ToString();
        }
    }
}