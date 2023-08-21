using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RdlHelper.Models
{
    public class ElementMaker
    {
        private static XmlDocument _xmlDoc = new XmlDocument();

        private string _namespace;

        public ElementMaker(string @namespace)
        {
            _namespace = @namespace;
        }

        public XmlElement MakeElement(string name, string text = null)
        {
            var el = _xmlDoc.CreateElement(name, _namespace);

            if (text != null)
            {
                var node = _xmlDoc.CreateTextNode(text);
                el.AppendChild(node);
            }
            return el;
        }
    }
}
