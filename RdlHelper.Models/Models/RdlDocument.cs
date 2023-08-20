using RdlHelper.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RdlHelper.Models
{
    public class RdlDocument
    {
        private string _rdlFilePath;
        private XmlDocument _xmlDocument;

        public static Dictionary<RdlParameterDataType, string> RdlDataTypeToString { get; }
        public string Namespace { get; }

        public RdlDocument(string rdlFilePath)
        {
            HandleFileNotExisting(rdlFilePath);

            _rdlFilePath = rdlFilePath;
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(_rdlFilePath);

            Namespace = GetNamespace(_xmlDocument);

            if (!IsValidRdlDocument())
            {
                throw new Exception("bad rld file.");
            }
        }

        private static string? GetNamespace(XmlDocument xmlDocument)
        {
            return xmlDocument.ChildNodes[1].NamespaceURI;
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

        private XmlElement GetParametersElement()
        {
            return (XmlElement)_xmlDocument.GetElementsByTagName("ReportParameters")[0];
        }

        public IEnumerable<RdlParameter> GetParameters()
        {  
            var reportParametersEl = GetParametersElement();

            var rdlParams = Enumerable.Range(0, reportParametersEl.ChildNodes.Count)
                .Select(i => new RdlParameter(this, (XmlElement)reportParametersEl.ChildNodes[i])).ToArray();

            return rdlParams;
        }

        public RdlParameter CreateParameter(string name, RdlParameterDataType dataType, string prompt = null, RdlParameterOption options = null)
        {
            var nameSpace = GetParametersElement().NamespaceURI;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(prompt))
            {
                prompt = name;
            }

            var reportParameter = _xmlDocument.CreateElement("ReportParameter", nameSpace);
            
            var nameAttr = _xmlDocument.CreateAttribute("Name", nameSpace);
            nameAttr.Value = name;
            reportParameter.Attributes.Append(nameAttr);

            var dataTypeElement = _xmlDocument.CreateElement("DataType", nameSpace);
            var dataTypeValue = _xmlDocument.CreateTextNode(RdlDataTypeToString[dataType]);
            dataTypeElement.AppendChild(dataTypeValue);
            reportParameter.AppendChild(dataTypeElement);

            var promptElement = _xmlDocument.CreateElement("Prompt", nameSpace);
            var promptValue = _xmlDocument.CreateTextNode(prompt);
            promptElement.AppendChild(promptValue);
            reportParameter.AppendChild(promptElement);

            var rdlParameter = new RdlParameter(this, reportParameter);

            rdlParameter.AllowBlank = options?.AllowsBlank ?? false;
            rdlParameter.Nullable = options?.IsNullable ?? false;
            rdlParameter.MultiValue = options?.MultiValue ?? false;

            throw new NotImplementedException();
        }

        public IEnumerable<XmlElement> GetParameterElements()
        {
            var reportParametersEl = GetParametersElement();

            var els = Enumerable.Range(0, reportParametersEl.ChildNodes.Count)
                .Select(i => (XmlElement)reportParametersEl.ChildNodes[i]);
            return els;
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
            var els = GetParameterElements().ToArray();
            var parametersEl = GetParametersElement();

            foreach (var item in els)
            {
                parametersEl.RemoveChild(item);
            }


            foreach (var item in newParameters)
            {
                parametersEl.AppendChild(item.Element);
            }
             
        }

        internal XmlElement CreateElement(string elementName, string namespaceURI)
        {
            return _xmlDocument.CreateElement(elementName, namespaceURI);
        }
    }

    
}
