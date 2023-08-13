using System;
using System.Xml;

namespace RdlHelper.Models
{ 
    public class RdlParameters
    {
        private XmlDocument _xmlDoc;
        private XmlElement? _parameterEl;

        internal RdlParameters(XmlDocument xmlDoc, XmlElement? xmlElement)
        {
            _xmlDoc = xmlDoc;
            _parameterEl = xmlElement;
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