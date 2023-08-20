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
            _rdlParameter = rdlParameter;
            Name = _rdlParameter.GetParameterName();
            Type = _rdlParameter.GetParameterType(); ;
            DefaultValues = new ObservableCollection<DefaultValueVm>(_rdlParameter.GetDefaultValues().Select(aa => new DefaultValueVm(aa)));
        }

        private RdlParameter _rdlParameter; 

        public string Name { get; set; }        
        public string Type { get; set; }
        public ObservableCollection<DefaultValueVm> DefaultValues { get; set; } 

        public bool DefaultValuesChanged => DefaultValues.Any(aa => aa.Chagned);

        public RdlParameter Parameter { get; internal set; }

        internal void ApplyChanges()
        {
            
        }
    }
}
