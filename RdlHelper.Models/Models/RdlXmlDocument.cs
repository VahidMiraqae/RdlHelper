using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RdlHelper.Models
{
    public class RdlXmlDocument
    {
        private string _rdlFilePath;
        private XmlDocument _xmlDocument;

        public RdlXmlDocument(string rdlFilePath)
        {
            HandleFileNotExisting(rdlFilePath);

            _rdlFilePath = rdlFilePath;
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(_rdlFilePath); 

            if (!IsValidRdlDocument())
            {
                throw new Exception("bad rld file.");
            }
        }

        private bool IsValidRdlDocument()
        {
            var hasReportParametersNode = _xmlDocument.GetElementsByTagName("ReportParameters").Count == 1;

            if (!hasReportParametersNode)
            {
                return false;
            }

            // todo: needs to be more sophisticated

            return true; 
        }

        private static void HandleFileNotExisting(string rdlFilePath)
        {
            var fileExists = File.Exists(rdlFilePath);

            if (!fileExists)
            {
                throw new FileNotFoundException();
            }
        }

        public IEnumerable<RdlParameter> GetReportParametersElements()
        {
            var parametersNode = _xmlDocument.GetElementsByTagName("ReportParameters");
              
            var reportParametersEl = (XmlElement)parametersNode[0];

            var rdlParams = Enumerable.Range(0, reportParametersEl.ChildNodes.Count)
                .Select(i => new RdlParameter(_xmlDocument, (XmlElement)reportParametersEl.ChildNodes[i])).ToArray();

            return rdlParams;
        }

        public void Overwrite()
        {
            _xmlDocument.Save(_rdlFilePath);
        }

        public void Save(string rdlFilePath)
        {
            var directory = Path.GetDirectoryName(rdlFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            _xmlDocument.Save(rdlFilePath);
        }


    }

    
}
