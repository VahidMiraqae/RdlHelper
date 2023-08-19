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
        public ReportParameterVm(RdlParameter rdlParameter)
        {
            _originalParam = rdlParameter;
            Name = _originalParam.GetParameterName();
            Type = _originalParam.GetParameterType(); ;
            DefaultValues = new ObservableCollection<ParameterDefaultValue>(_originalParam.GetDefaultValues().Select(aa => new ParameterDefaultValue(aa)));
        }

        private RdlParameter _originalParam;

        public string Name { get; set; }        
        public string Type { get; set; }
        public ObservableCollection<ParameterDefaultValue> DefaultValues { get; set; }
        public RdlParameter RdlParameter { get; set; }

        public bool DefaultValuesChanged => DefaultValues.Any(aa => aa.Chagned);

        internal void ApplyChanges()
        {
            if (DefaultValuesChanged)
            {
                
            }
        }
    }
}
