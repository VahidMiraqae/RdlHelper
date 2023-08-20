// See https://aka.ms/new-console-template for more information
using RdlHelper.Models;
using System.Xml;

Console.WriteLine("Hello, World!");

var rdlFilePath = @"C:\Users\Vahid\Desktop\SampleReport.rdl";



var doc = new XmlDocument();
doc.Load(rdlFilePath);

var nsManager = new XmlNamespaceManager(doc.NameTable);
nsManager.AddNamespace("ns", Report.Namespace);


var nodes = doc.SelectNodes("//ns:ReportParameter", nsManager);

var @params = Enumerable.Range(0, nodes.Count).Select(i => new ReportParameter((XmlElement)nodes[i])).ToList();


Console.ReadLine();