using RdlHelper.Models;
using RdlHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RdlHelper.Views
{
    internal class ReportParameterVm : BaseVm
    {
        private bool _isMultiValue;
        private string _type;
        private string _name;
        private bool _canAddDefaultValue;

        public ReportParameterVm(ReportParameter rdlParameter)
        {
            Parameter = rdlParameter;
            Name = Parameter.GetParameterName();
            Type = Parameter.GetParameterType(); ;
            DefaultValues = new ObservableCollection<DefaultValueVm>(Parameter.GetDefaultValues().Select(aa => new DefaultValueVm(aa)));

        }

        public ReportParameter Parameter { get; set; }

        public string Name { get => _name; set { _name = value; OnPropChanged(); } }
        public string Type { get => _type; set { _type = value; OnPropChanged(); } }
        public bool IsMultiValue { get => _isMultiValue; set { _isMultiValue = value; OnPropChanged(); } }
        public bool CanAddDefaultValue { get => _canAddDefaultValue; set { _canAddDefaultValue = value; OnPropChanged(); } }

        public ObservableCollection<DefaultValueVm> DefaultValues { get; set; } 

        public bool DefaultValuesChanged => DefaultValues.Any(aa => aa.Chagned);
         

        internal void ApplyChanges()
        {
            if (DefaultValuesChanged)
            {
                Parameter.SetDefaultValues(DefaultValues.Select(dv => dv.DefaultValue));
            }
        }
    }
}
