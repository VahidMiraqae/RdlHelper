// See https://aka.ms/new-console-template for more information
using RdlHelper.Models;
using RdlHelper.Models.Models;

Console.WriteLine("Hello, World!");

var rdlFilePath = @"C:\Users\Vahid\Desktop\SampleReport.rdl";

var rdlDoc = new RdlDocument(rdlFilePath);

rdlDoc.CreateParameter("MyParameter", RdlParameterDataType.Text);