using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RdlHelper.Models
{
    public static class Extensions
    {
        
        public static XmlElement ToXml(this ReportParameter parameter)
        {
            var elMaker = new ElementMaker(parameter.Namespace);

            var rp = elMaker.MakeElement("ReportParameter");
            rp.SetAttribute("Name", parameter.Name);

            rp.AppendChild(elMaker.MakeElement("DataType", parameter.DataType.ToString()));
            rp.AppendChild(elMaker.MakeElement("Prompt", parameter.Prompt));

            if (parameter.Nullable)
            {
                rp.AppendChild(elMaker.MakeElement("Nullable", "true"));
            }

            if (parameter.AllowBlank)
            {
                rp.AppendChild(elMaker.MakeElement("AllowBlank", "true"));
            }

            if (parameter.Hidden)
            {
                rp.AppendChild(elMaker.MakeElement("Hidden", "true"));
            }

            XmlElement defaultValueEl = null;

            if (parameter.DefaultValueType != ValueProvidingType.None)
            {
                defaultValueEl = elMaker.MakeElement("DefaultValue");
                rp.AppendChild(defaultValueEl);
            }

            switch (parameter.DefaultValueType)
            {
                case ValueProvidingType.Literals:
                    MakeLiteralDefaultValueElements();
                    break;
                case ValueProvidingType.FromQuery:
                    MakeFromQueryDefaultValuesElements();
                    break;
            }


            XmlElement validValueEl = null;

            if (parameter.ValidValuesType != ValueProvidingType.None)
            {
                validValueEl = elMaker.MakeElement("ValidValues");
                rp.AppendChild(validValueEl);
            }

            switch (parameter.ValidValuesType)
            {
                case ValueProvidingType.Literals:
                    MakeLiteralValidValueElements();
                    break;
                case ValueProvidingType.FromQuery:
                    MakeFromQueryValidValuesElements();
                    break;
            }

            return rp;



            void MakeFromQueryDefaultValuesElements()
            {
                var dataSetReferenceEl = elMaker.MakeElement("DataSetReference");
                dataSetReferenceEl.AppendChild(elMaker.MakeElement("DataSetName", parameter.FromQueryDefaultValue.DataSetName));
                dataSetReferenceEl.AppendChild(elMaker.MakeElement("ValueField", parameter.FromQueryDefaultValue.ValueField));
                defaultValueEl.AppendChild(dataSetReferenceEl);
            }

            void MakeLiteralDefaultValueElements()
            {
                var valuesEl = elMaker.MakeElement("Values");
                foreach (var defaultValue in parameter.DefaultValues)
                {
                    valuesEl.AppendChild(elMaker.MakeElement("Value", defaultValue));
                }
                defaultValueEl.AppendChild(valuesEl);
            }

            void MakeFromQueryValidValuesElements()
            {
                var dataSetReferenceEl = elMaker.MakeElement("DataSetReference");
                dataSetReferenceEl.AppendChild(elMaker.MakeElement("DataSetName", parameter.FromQueryValidParameterValue.DataSetName));
                dataSetReferenceEl.AppendChild(elMaker.MakeElement("ValueField", parameter.FromQueryValidParameterValue.ValueField));
                dataSetReferenceEl.AppendChild(elMaker.MakeElement("LabelField", parameter.FromQueryValidParameterValue.LabelField));
                validValueEl.AppendChild(dataSetReferenceEl);
            }

            void MakeLiteralValidValueElements()
            {
                var parameterValuesEl = elMaker.MakeElement("ParameterValues");
                foreach (var parameterValue in parameter.ParameterValues)
                {
                    var parameterValueEl = elMaker.MakeElement("ParameterValue");

                    var valueEl = elMaker.MakeElement("Value", parameterValue.Key);
                    var label = elMaker.MakeElement("Lebel", parameterValue.Value);

                    parameterValueEl.AppendChild(valueEl);
                    parameterValueEl.AppendChild(label);

                    parameterValuesEl.AppendChild(parameterValueEl);
                }
                validValueEl.AppendChild(parameterValuesEl);
            }
        }

        

       

    }
}
