﻿using RdlHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using static RdlHelper.Models.RdlXmlDocument;

namespace RdlHelper.ViewModels.RdlCommands
{
    //[Obsolete]
    internal class ShowParametersVm : RdlCommand
    {
        public ShowParametersVm(MainVm mainVm) : base(mainVm)
        {
        }

        public override string Name => "Show\nParameters";


        public override string Perform(IEnumerable<string> filePaths)
        {
            var sb = new StringBuilder();

            foreach (var filePath in filePaths)
            {
                var rdlXmlDoc = new RdlXmlDocument(filePath);
                var parameters = rdlXmlDoc.GetReportParametersElements();

                foreach (var parameter in parameters)
                {
                    parameter.MakeVisible();
                }

                rdlXmlDoc.Overwrite();

                sb.AppendLine($"'{System.IO.Path.GetFileName(filePath)}' => visible parameters.");
            }

            return sb.ToString();
        }
    }
}