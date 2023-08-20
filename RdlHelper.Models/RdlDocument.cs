using RdlHelper.Models.Services;
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
    public class RdlDocument
    {
        private string _rdlFilePath;
        private XmlDocument _xmlDocument;

        public static Dictionary<RdlParameterDataType, string> RdlDataTypeToString { get; }

        private ReportParameterCreator _reportParameterCreator;

        public string Namespace { get; }
        protected XmlElement ReportParametersElement { get; }

        public RdlDocument(string rdlFilePath)
        {
            HandleFileNotExisting(rdlFilePath);

            _rdlFilePath = rdlFilePath;
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(_rdlFilePath);
            _reportParameterCreator = new ReportParameterCreator(_xmlDocument);

            Namespace = _xmlDocument.ChildNodes[1].NamespaceURI;

            if (!IsValidRdlDocument())
            {
                throw new Exception("bad rld file.");
            }

            ReportParametersElement = (XmlElement)_xmlDocument.GetElementsByTagName("ReportParameters")[0];
        }
         

        static RdlDocument()
        {
            var type = typeof(RdlParameterDataType);
            RdlDataTypeToString = type.GetFields().Where(aa => aa.FieldType.FullName == type.FullName)
                .ToDictionary(bb => (RdlParameterDataType)bb.GetValue(null), aa => aa.GetCustomAttribute<EnumMemberAttribute>().Value);
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
         

        public IEnumerable<RdlParameter> GetParameters()
        {  
            var rdlParams = Enumerable.Range(0, ReportParametersElement.ChildNodes.Count)
                .Select(i => new RdlParameter(_reportParameterCreator, (XmlElement)ReportParametersElement.ChildNodes[i])).ToArray();

            return rdlParams;
        }

        public RdlParameter CreateParameter(string name, RdlParameterDataType dataType, string prompt = null, RdlParameterOption options = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
             

            var rdlParam = _reportParameterCreator.Create(name, dataType, prompt, options);
             
            return rdlParam;
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

        public void ResetParameters(IEnumerable<RdlParameter> newParameters)
        {
            // any parameter either existed before or not
            // didn't exist: add it.
            // existed: either changed or not
            //      changed: overwrite it
            //      didn't change: leave it alone

            var els = GetParameterElements().ToArray(); 

            foreach (var item in els)
            {
                ReportParametersElement.RemoveChild(item);
            }


            foreach (var item in newParameters)
            {
                ReportParametersElement.AppendChild(item.Element);
            }
             
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
