using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace RdlHelper.Models
{ 
    public class ReportParameter
    {
        private bool _allowBlank;
        private bool _isMultiValue;

        public RdlParameterDataType DataType { get; }
        public string Prompt { get; }
        public string Name { get; }
        
        public bool AllowBlank { get => _allowBlank; set { _allowBlank = DecideIfAllowedBlankCanBeSetTo(value); } }

        public bool Hidden {get; set;}

        public bool Nullable { get; set; }
        public bool IsMultiValue { get => _isMultiValue; set { _isMultiValue = DecideIfIsMultiValueCanBeSetTo(value); } }

        public List<string> DefaultValues { get; private set; }

        public ReportParameter(XmlElement xmlElement)
        {
            var nsManager = new XmlNamespaceManager(new NameTable());
            nsManager.AddNamespace("ns", Report.Namespace); 

            DataType = Enum.Parse<RdlParameterDataType>(xmlElement.SelectSingleNode("ns:DataType", nsManager).InnerText);
            Prompt = xmlElement.SelectSingleNode("ns:Prompt", nsManager).InnerText;
            Name = xmlElement.GetAttribute("Name");


            Hidden = xmlElement.SelectSingleNode("ns:Hidden", nsManager) != null;
            Nullable = xmlElement.SelectSingleNode("ns:Nullable", nsManager) != null;
            AllowBlank = xmlElement.SelectSingleNode("ns:AllowBlank", nsManager) != null;

            var defaultValueNodes = xmlElement.SelectNodes("ns:DefaultValue/ns:Values/ns:Value", nsManager);
            DefaultValues = Enumerable.Range(0, defaultValueNodes.Count).Select(i => defaultValueNodes[i].InnerText).ToList();

            IsMultiValue = xmlElement.SelectSingleNode("ns:MultiValue", nsManager) != null;

        }

        public ReportParameter()
        {

        }

        public XmlElement ToXml()
        {
            throw new NotImplementedException();
        }
         
        private bool DecideIfAllowedBlankCanBeSetTo(bool value)
        {
            if (value == true && DataType == RdlParameterDataType.String)
            {
                return true;
            }
            return false;
        }


        private bool DecideIfIsMultiValueCanBeSetTo(bool value)
        {
            if (value == true)
            {
                Nullable = false;
                return true;
            }

            if (DefaultValues.Any())
            {
                DefaultValues.RemoveRange(1, DefaultValues.Count - 1);
            }
            return false;
        }


    }
}