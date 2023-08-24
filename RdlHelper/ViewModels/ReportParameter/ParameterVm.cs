using RdlHelper.Models;
using RdlHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RdlHelper.ViewModels.ReportParameterViewModels
{
    internal class ParameterVm : BaseVm
    {
        private bool _isMultiValue;
        private ParameterDataType _type;
        private string _name; 
        private int _order;
        private ValueProvidingType _dvt;
        private bool _allowBlank;
        private bool _allowBlankEnabled;

        public ParameterVm(ReportParameter rdlParameter)
        {
            Parameter = rdlParameter;
            Name = rdlParameter.Name;
            Type = rdlParameter.DataType;
            DefaultValues = new ObservableCollection<DefaultValueVm>(Parameter.DefaultValues.Select(aa => new DefaultValueVm(aa)));
            Order = rdlParameter.Order;
            DefaultValueType = rdlParameter.DefaultValueType;
            IsMultiValue = rdlParameter.IsMultiValue;
            AllowBlank = rdlParameter.AllowBlank;
        }

        public ReportParameter Parameter { get; set; }

        public string Name { get => _name; set { _name = value; OnPropChanged(); } }
        public ParameterDataType Type { get => _type; set { _type = value; OnTypePropChange();  } }
        public ObservableCollection<DefaultValueVm> DefaultValues { get; set; }
        public int Order { get => _order; set { _order = value; OnPropChanged(); } }
        public ValueProvidingType DefaultValueType { get => _dvt; set { _dvt = value; OnPropChanged(); } }
        public bool IsMultiValue { get => _isMultiValue; set { _isMultiValue = value; OnPropChanged(); } } 
        public bool AllowBlank { get => _allowBlank; set { _allowBlank = value; OnPropChanged(); } }
        public bool IsAllowBlankAvailable { get => _allowBlankEnabled; set { _allowBlankEnabled = value; OnIsAllowBlankAvaialbePropChanged(); } }


        private void OnTypePropChange([CallerMemberName] string propName = null)
        {
            OnPropChanged(propName);
            IsAllowBlankAvailable = Parameter.IsEmptyStringAvailable(Type);
        }

        private void OnIsAllowBlankAvaialbePropChanged([CallerMemberName] string propName = null)
        {
            OnPropChanged(propName);
            if (!IsAllowBlankAvailable)
            {
                AllowBlank = false;
            }
        }

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
