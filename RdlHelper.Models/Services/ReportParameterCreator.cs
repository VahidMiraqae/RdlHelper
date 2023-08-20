using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RdlHelper.Models.Services
{
    public class ReportParameterCreator
    {
        private string _namespace;
        private XmlDocument _xmlDocument;

        public ReportParameterCreator(XmlDocument xmlDocument)
        {
            _xmlDocument = xmlDocument;
            _namespace = _xmlDocument.ChildNodes[1].NamespaceURI;
        }

        internal RdlParameter Create(string name, RdlParameterDataType dataType, string prompt, RdlParameterOption options)
        {
            var rpEl = CreateReportParameterElement(name);

            XmlElement dataTypeElement = CreateDataTypeElement(dataType);
            rpEl.AppendChild(dataTypeElement);

            if (string.IsNullOrWhiteSpace(prompt))
            {
                prompt = name;
            }

            XmlElement promptElement = CreatePromptElement(prompt);
            rpEl.AppendChild(promptElement);

            var rdlParameter = new RdlParameter(this, rpEl);

            rdlParameter.AllowBlank = options?.AllowsBlank ?? false;
            rdlParameter.Nullable = options?.IsNullable ?? false; 

            return rdlParameter;
        }

        private XmlElement CreatePromptElement(string prompt)
        {
            var promptElement = _xmlDocument.CreateElement("Prompt", _namespace);
            var promptValue = _xmlDocument.CreateTextNode(prompt);
            promptElement.AppendChild(promptValue);
            return promptElement;
        }

        private XmlElement CreateDataTypeElement(RdlParameterDataType dataType)
        {
            var dataTypeElement = _xmlDocument.CreateElement("DataType", _namespace);
            var dataTypeValue = _xmlDocument.CreateTextNode(RdlDocument.RdlDataTypeToString[dataType]);
            dataTypeElement.AppendChild(dataTypeValue);
            return dataTypeElement;
        }

        private XmlElement CreateReportParameterElement(string name)
        {
            var reportParameter = _xmlDocument.CreateElement("ReportParameter", _namespace);

            var nameAttr = _xmlDocument.CreateAttribute("Name", _namespace);
            nameAttr.Value = name;
            reportParameter.Attributes.Append(nameAttr);
            return reportParameter;
        }

        internal XmlElement CreateHiddenElement()
        {
            var el = CreateElementWithContent("Hidden", "true");
            return el;
        }

        internal XmlNode CreateValueElement(string newValue)
        {
            var el = CreateElementWithContent("Value", newValue);
            return el;
        }

        private XmlElement CreateElementWithContent(string elementName, string elementContent)
        {
            var el = _xmlDocument.CreateElement(elementName, _namespace);
            var txt = _xmlDocument.CreateTextNode(elementContent);
            el.AppendChild(txt);
            return el;
        }
    }
}
