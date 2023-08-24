using RdlHelper.Models;
using RdlHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RdlHelper.ViewModels.ReportParameterViewModels
{
    internal class ParameterVm : BaseVm
    {
        private bool _isMultiValue;
        private ParameterDataType _type;
        private string _name;
        private bool _canAddDefaultValue;
        private int _order;
        private ValueProvidingType _dvt;

        public ParameterVm(ReportParameter rdlParameter)
        {
            Parameter = rdlParameter;
            Name = rdlParameter.Name;
            Type = rdlParameter.DataType;
            DefaultValues = new ObservableCollection<DefaultValueVm>(Parameter.DefaultValues.Select(aa => new DefaultValueVm(aa)));
            Order = rdlParameter.Order;
            DefaultValueType = rdlParameter.DefaultValueType;
        }

        public ReportParameter Parameter { get; set; }

        public int Order { get => _order; set { _order = value; OnPropChanged(); } }

        public ValueProvidingType DefaultValueType { get => _dvt; set { _dvt = value; OnPropChanged(); } }

        public string Name { get => _name; set { _name = value; OnPropChanged(); } }
        public ParameterDataType Type { get => _type; set { _type = value; OnPropChanged(); } }
        public bool IsMultiValue { get => _isMultiValue; set { _isMultiValue = value; OnPropChanged(); } }
        public bool CanAddDefaultValue { get => _canAddDefaultValue; set { _canAddDefaultValue = value; OnPropChanged(); } }


        public ObservableCollection<DefaultValueVm> DefaultValues { get; set; }

        public bool DefaultValuesChanged => DefaultValues.Any(aa => aa.Chagned);


        internal void ApplyChanges()
        {
            if (DefaultValuesChanged)
            {
                //Parameter.SetDefaultValues(DefaultValues.Select(dv => dv.DefaultValue));
            }
        }
    }
}
