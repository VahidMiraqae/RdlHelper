using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RdlHelper.Models
{
    public class Report
    {
        public const string Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";

        private XmlDocument _xmlDocument;
        private RdlXmlElementSelector _es;

        public string LoadedFromFilePath { get; set; }
        public IEnumerable<ReportParameter> Parameters { get; set; } 
          
        protected XmlElement ReportParametersElement { get; }

        public Report(string rdlFilePath)
        {
            HandleFileNotExisting(rdlFilePath);

            LoadedFromFilePath = rdlFilePath;

            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(LoadedFromFilePath);

            _es = new RdlXmlElementSelector(_xmlDocument);

            if (!IsValidRdlDocument())
            {
                throw new Exception("bad rld file.");
            }



            Parameters = ReadParameters(_xmlDocument);

            // ReportParametersElement = (XmlElement)_xmlDocument.GetElementsByTagName("ReportParameters")[0];
        }

        private IEnumerable<ReportParameter> ReadParameters(XmlDocument xmlDocument)
        {
            var rps = _es.GetReportParameterElements().Select(xl => new ReportParameter(xl)).ToList();
            return rps;
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
         
           

        public void Overwrite()
        {
            _xmlDocument.Save(LoadedFromFilePath);
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

        public void ResetParameters(IEnumerable<ReportParameter> newParameters)
        {
            // any parameter either existed before or not
            // didn't exist: add it.
            // existed: either changed or not
            //      changed: overwrite it
            //      didn't change: leave it alone

            
             
        }

        internal XmlElement CreateElement(string elementName)
        {
            return _xmlDocument.CreateElement(elementName, Namespace);
        }


        private IEnumerable<XmlElement> GetParameterElements()
        {
            var els = Enumerable.Range(0, ReportParametersElement.ChildNodes.Count)
                .Select(i => (XmlElement)ReportParametersElement.ChildNodes[i]);
            return els;
        }
    }

    
}
