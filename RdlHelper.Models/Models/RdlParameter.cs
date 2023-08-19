using System;
using System.Xml;

namespace RdlHelper.Models
{ 
    public class RdlParameter
    {
        private bool _hidden;

        public RdlXmlDocument RdlDocument { get; }
        public XmlElement Element { get; }
        public bool AllowBlank { get; internal set; }
        public bool Hidden { get => _hidden; set
            {
                if (value)
                {
                    MakeHidden();
                }
                else
                {
                    MakeVisible();
                }
                _hidden = value;
            }
        }

        public bool Nullable { get; internal set; }
        public bool MultiValue { get; internal set; }

        internal RdlParameter(RdlXmlDocument rdlXmlDoc, XmlElement xmlElement)
        {
            RdlDocument = rdlXmlDoc;
            Element = xmlElement;
        }

        public IEnumerable<string> GetDefaultValues()
        {
            var valueEls = Element.GetElementsByTagName("Value");

            foreach (var valueEl in valueEls)
            {
                yield return ((XmlElement)valueEl).InnerText;
            }

        }

        public string GetParameterName()
        {
            var name = Element.GetAttribute("Name");
            return name;
        }

        public string GetParameterType()
        {
            var dtEl = Element.GetElementsByTagName("DataType")[0];
            return dtEl.InnerText;
        }

        private void MakeHidden()
        {
            var hiddenNode = Element.GetElementsByTagName("Hidden");

            if (hiddenNode.Count == 0)
            {
                var xmlEl = RdlDocument.CreateElement("Hidden", Element.NamespaceURI);
                xmlEl.InnerText = "true";

                Element.AppendChild(xmlEl);
            }
        }

        private void MakeVisible()
        {
            var hiddenNode = Element.GetElementsByTagName("Hidden");

            if (hiddenNode.Count != 0)
            {
                Element.RemoveChild(hiddenNode[0]);
            }
        }
    }
}