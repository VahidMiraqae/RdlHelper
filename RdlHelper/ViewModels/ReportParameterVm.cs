using RdlHelper.Models;
using RdlHelper.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RdlHelper.Views
{
    internal class ReportParameterVm : BaseVm
    {
        public ReportParameterVm(RdlParameter rdlParameter)
        {
            _rdlParameter = rdlParameter;
            Name = _rdlParameter.GetParameterName();
            Type = _rdlParameter.GetParameterType(); ;
            DefaultValues = new ObservableCollection<ParameterDefaultValue>(_rdlParameter.GetDefaultValues().Select(aa => new ParameterDefaultValue(aa)));
        }

        private RdlParameter _rdlParameter;
        private bool _deleted;

        public string Name { get; set; }        
        public string Type { get; set; }
        public ObservableCollection<ParameterDefaultValue> DefaultValues { get; set; }
        public bool Deleted { get => _deleted; set { _deleted = value; OnPropChanged(); } }

        public bool DefaultValuesChanged => DefaultValues.Any(aa => aa.Chagned);
    }
}
