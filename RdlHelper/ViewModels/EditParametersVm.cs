using RdlHelper.Models;
using RdlHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RdlHelper.Views
{
    internal class EditParametersVm : BaseVm
    {
        public EditParametersVm(IEnumerable<RdlParameter> parameters) 
        {
            _originalParams = parameters.Select(p => new ReportParameterVm(p)).ToDictionary(aa => aa.Name, aa => aa);

            Parameters = new ObservableCollection<ReportParameterVm>(_originalParams.Values);
        }
        public ObservableCollection<ReportParameterVm> Parameters { get; set; }

        private Dictionary<string, ReportParameterVm> _originalParams; 

        internal void ApplyChanges()
        {
             foreach (var parameter in Parameters)
            {
                parameter.ApplyChanges();
            }
        }

        internal IEnumerable<RdlParameter> GetParameters()
        {
            return Parameters.Select(p => p.Parameter);
        }
    }
}
