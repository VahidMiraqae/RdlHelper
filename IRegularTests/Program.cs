using System.Xml;
using System.Xml.XPath;

// MakingADocument();

var @namespace = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
var xmlFilePath = @"C:\Users\Vahid\Desktop\SampleReport.rdl";
var doc = new XmlDocument();
doc.Load(xmlFilePath);
//var nsmgr = new XmlNamespaceManager(doc.NameTable);
var nsmgr = new XmlNamespaceManager(new NameTable());
nsmgr.AddNamespace("a", @namespace);
var @params = doc.SelectSingleNode("/a:Report/a:ReportParameters", nsmgr);



var allParams = @params.SelectNodes("a:ReportParameter", nsmgr);


foreach (XmlElement item in allParams)
{
    var name = item.GetAttribute("Name");
    var hiddenEl = item.SelectNodes("a:Hidden", nsmgr);
}

Console.ReadLine();

static void MakingADocument()
{
    var doc = new XmlDocument();
    var @namespace = "Something";
    var persons = doc.CreateElement("Persons", @namespace);
    var person = doc.CreateElement("Person", @namespace);
    var name = doc.CreateElement("Name", @namespace);
    name.InnerText = "Vahid";
    person.AppendChild(name);
    persons.AppendChild(person);
    doc.AppendChild(persons);

    doc.Save("file.xml");
}