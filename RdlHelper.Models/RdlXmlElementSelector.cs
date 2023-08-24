using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RdlHelper.Models
{
    internal class RdlXmlElementSelector
    { 
        private XmlNamespaceManager _nsManager;
        private XmlDocument _xmlDoc;
        private XmlNode? _reportEl;

        public RdlXmlElementSelector(XmlDocument rdlXmlDoc)
        {
            _xmlDoc = rdlXmlDoc;
            _reportEl = rdlXmlDoc.ChildNodes[1];
            var ns = _reportEl.NamespaceURI;
            _nsManager = new XmlNamespaceManager(new NameTable());
            _nsManager.AddNamespace(Ns, ns);
        }

        public string Ns { get; } = "ns";

        public XmlElement[] GetReportParameterElements()
        {
            var nodes = _reportEl.SelectNodes("ns:ReportParameters/ns:ReportParameter", _nsManager);
            return Enumerable.Range(0, nodes.Count).Select(i => (XmlElement)nodes[i]).ToArray();
        }
    }
}
