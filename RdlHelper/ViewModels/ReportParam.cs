using RdlHelper.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RdlHelper.Views
{
    internal class ReportParam : BaseVm
    {
        public ReportParam(string name, string type, IEnumerable<string> defaultValues)
        {
            Name = name;
            Type = type;
            DefaultValues = defaultValues.Select(aa => new ParameterDefaultValue(aa)).ToList();
        }

        public string Name { get; set; }        
        public string Type { get; set; }
        public List<ParameterDefaultValue> DefaultValues { get; set; }
    }
}
