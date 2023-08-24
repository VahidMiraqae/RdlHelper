using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace RdlHelper.Models
{ 
    public class ReportParameter
    {
        
        private bool _allowBlank;
        private bool _isMultiValue;

        public string Namespace { get; private set; }
        public ParameterDataType DataType { get; }
        public string Prompt { get; }
        public string Name { get; }
        
        public bool AllowBlank { get => _allowBlank; set { _allowBlank = DecideIfAllowedBlankCanBeSetTo(value); } }

        public bool Hidden {get; set;}

        public int Order { get; set;}

        public bool Nullable { get; set; }
        public bool IsMultiValue { get => _isMultiValue; set { _isMultiValue = DecideIfIsMultiValueCanBeSetTo(value); } }

        public List<string> DefaultValues { get; private set; } = new List<string>();
        public Dictionary<string,string> ParameterValues { get; set; }

        public ValueProvidingType DefaultValueType { get; set; }
        public ValueProvidingType ValidValuesType { get; set; }

        public QueryReference FromQueryDefaultValue { get; internal set; }
        public QueryReference FromQueryValidParameterValue { get; internal set; }

        public ReportParameter(int order, XmlElement xmlElement)
        {
            Namespace = xmlElement.NamespaceURI;

            Order = order;

            var nsManager = new XmlNamespaceManager(new NameTable());
            nsManager.AddNamespace("ns", xmlElement.NamespaceURI); 

            DataType = Enum.Parse<ParameterDataType>(xmlElement.SelectSingleNode("ns:DataType", nsManager).InnerText);
            Prompt = xmlElement.SelectSingleNode("ns:Prompt", nsManager).InnerText;
            Name = xmlElement.GetAttribute("Name");


            Hidden = xmlElement.SelectSingleNode("ns:Hidden", nsManager) != null;
            Nullable = xmlElement.SelectSingleNode("ns:Nullable", nsManager) != null;
            AllowBlank = xmlElement.SelectSingleNode("ns:AllowBlank", nsManager) != null;

            var defaultValueNode = xmlElement.SelectSingleNode("ns:DefaultValue", nsManager);

            if (defaultValueNode == null)
            {
                DefaultValueType = ValueProvidingType.None;
            }
            else
            {
                var ValuesNode = xmlElement.SelectSingleNode("ns:DefaultValue/ns:Values", nsManager);

                if (ValuesNode == null)
                {
                    DefaultValueType = ValueProvidingType.FromQuery;

                    var dataSetReferenceNode = xmlElement.SelectSingleNode("ns:DefaultValue/ns:DataSetReference", nsManager);
                    FromQueryDefaultValue = new QueryReference()
                    {
                        DataSetName = dataSetReferenceNode.SelectSingleNode("ns:DataSetName", nsManager).InnerText,
                        ValueField = dataSetReferenceNode.SelectSingleNode("ns:ValueField", nsManager).InnerText
                    };
                }
                else
                {
                    DefaultValueType = ValueProvidingType.Literals;

                    var valueNodes = ValuesNode.SelectNodes("ns:Value", nsManager);
                    DefaultValues = Enumerable.Range(0, valueNodes.Count).Select(i => valueNodes[i].InnerText).ToList();
                }

            }  

            IsMultiValue = xmlElement.SelectSingleNode("ns:MultiValue", nsManager) != null;

        }

        public ReportParameter(string name, string prompt, ParameterDataType dataType)
        {
            Name = name;
            Prompt = prompt;
            DataType = dataType;
        }
         
        private bool DecideIfAllowedBlankCanBeSetTo(bool value)
        {
            if (value == true && IsEmptyStringAvailable(DataType))
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

        public bool IsEmptyStringAvailable(ParameterDataType type)
        {
            return type == ParameterDataType.String;
        }
    }
}