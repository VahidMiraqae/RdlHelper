using System;
using System.Xml;

namespace RdlHelper.Models
{ 
    public class RdlParameter
    {
        private XmlDocument _xmlDoc;
        private XmlElement? _parameterEl;

        internal RdlParameter(XmlDocument xmlDoc, XmlElement? xmlElement)
        {
            _xmlDoc = xmlDoc;
            _parameterEl = xmlElement;
        }

        public IEnumerable<string> GetDefaultValues()
        {
            var valueEls = _parameterEl.GetElementsByTagName("Value");

            foreach (var valueEl in valueEls)
            {
                yield return ((XmlElement)valueEl).InnerText;
            }

        }

        public string GetParameterName()
        {
            var name = _parameterEl.GetAttribute("Name");
            return name;
        }

        public string GetParameterType()
        {
            var dtEl = _parameterEl.GetElementsByTagName("DataType")[0];
            return dtEl.InnerText;
        }

        public void MakeHidden()
        {
            var hiddenNode = _parameterEl.GetElementsByTagName("Hidden");

            if (hiddenNode.Count == 0)
            {
                var xmlEl = _xmlDoc.CreateElement("Hidden", _parameterEl.NamespaceURI);
                xmlEl.InnerText = "true";

                _parameterEl.AppendChild(xmlEl);
            }
        }

        public void MakeVisible()
        {
            var hiddenNode = _parameterEl.GetElementsByTagName("Hidden");

            if (hiddenNode.Count != 0)
            {
                _parameterEl.RemoveChild(hiddenNode[0]);
            }
        }
    }
}