using RdlHelper.Models.Services;
using System;
using System.Xml;

namespace RdlHelper.Models
{ 
    public class RdlParameter
    {
        private bool _hidden;

        public ReportParameterCreator _reportParamCreator { get; }
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

        internal RdlParameter(ReportParameterCreator reportParamCreator, XmlElement xmlElement)
        {
            _reportParamCreator = reportParamCreator;
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
                var xmlEl = _reportParamCreator.CreateHiddenElement();

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

        public void SetDefaultValues(IEnumerable<string> newValues)
        {
            var vEl = (XmlElement)Element.GetElementsByTagName("Values")[0];

            var values = Enumerable.Range(0, vEl.ChildNodes.Count).Select(i => vEl.ChildNodes[i]).ToArray();

            foreach (var v in values)
            {
                vEl.RemoveChild(v);
            }

            foreach (var newValue in newValues)
            {
                var el = _reportParamCreator.CreateValueElement(newValue);
                vEl.AppendChild(el);
            }

        }
    }
}