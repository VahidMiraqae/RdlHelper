using RdlHelper.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RdlHelper.Views
{
    internal class ReportParam : BaseVm
    {
        public ReportParam(string name, string type, IEnumerable<string> defaultValues)
        {
            Name = name;
            Type = type;
            DefaultValues = new ObservableCollection<ParameterDefaultValue>(defaultValues.Select(aa => new ParameterDefaultValue(aa)));
        }

        public string Name { get; set; }        
        public string Type { get; set; }
        public ObservableCollection<ParameterDefaultValue> DefaultValues { get; set; }
    }
}
