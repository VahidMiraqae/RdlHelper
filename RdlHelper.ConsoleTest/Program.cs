// See https://aka.ms/new-console-template for more information
using RdlHelper.Models;
using System.Xml;

Console.WriteLine("Hello, World!");

var rdlFilePath = @"C:\Users\Vahid\Desktop\SampleReport.rdl";
var rdlFilePath2 = @"D:\All_TFS\Reports\Reports\Reports\Sample.rdl";


// CreateReportParameter();

 CreateReportParameterFromExistingXmlElement(rdlFilePath2);

Console.ReadLine();

static void CreateReportParameter()
{
    var rp = new ReportParameter("Vahid", "Vahidooo", ParameterDataType.DateTime);
    var el = rp.ToXml();
}

static void CreateReportParameterFromExistingXmlElement(string rdlFilePath2)
{
    var doc = new XmlDocument();
    doc.Load(rdlFilePath2);

    var el = (XmlElement)doc.GetElementsByTagName("ReportParameter")[0];

    var rp = new ReportParameter(el);

    var xmlt = rp.ToXml();
}