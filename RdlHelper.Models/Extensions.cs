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
        private static XmlDocument _xmlDoc = new XmlDocument();
        public static XmlElement ToXml(this ReportParameter parameter)
        {
            var xml = @"<ReportParameter Name=""CRM_FullName"">
                          <DataType>String</DataType>
                          <Nullable>true</Nullable>
                          <AllowBlank>true</AllowBlank>
                          <Prompt>CRM_FullName</Prompt>
                          <DefaultValue>
                            <Values>
                              <Value>Satish Kadam</Value>
                            </Values>
                          </DefaultValue>
                          <Hidden>true</Hidden>
                        </ReportParameter>";

            var rp = MakeElement("ReportParameter");
            rp.SetAttribute("Name", parameter.Name);

            rp.AppendChild(MakeElement("DataType", parameter.DataType.ToString()));
            rp.AppendChild(MakeElement("Prompt", parameter.Prompt));

            if (parameter.Nullable)
            {
                rp.AppendChild(MakeElement("Nullable", "true"));
            }

            if (parameter.AllowBlank)
            {
                rp.AppendChild(MakeElement("AllowBlank", "true"));
            }

            if (parameter.Hidden)
            {
                rp.AppendChild(MakeElement("Hidden", "true"));
            }

            if (parameter.DefaultValues.Any())
            {

            }


        }

        private static XmlElement MakeElement(string name, string text = null)
        {
            var el = _xmlDoc.CreateElement(name, Report.Namespace);

            if (text != null)
            {
                var node = _xmlDoc.CreateTextNode(text);
                el.AppendChild(node);
            }
            return el;
        }

    }
}
