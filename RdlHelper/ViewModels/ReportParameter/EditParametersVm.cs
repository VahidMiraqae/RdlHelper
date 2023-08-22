using RdlHelper.Models;
using RdlHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RdlHelper.ViewModels.ReportParameter
{
    internal class EditParametersVm : BaseVm
    {
        public EditParametersVm(IEnumerable<ReportParameter> parameters)
        {
            _originalParams = parameters.Select(p => new ReportParameterVm(p)).ToDictionary(aa => aa.Name, aa => aa);

            Parameters = new ObservableCollection<ParameterVm>(_originalParams.Values);
        }
        public ObservableCollection<ParameterVm> Parameters { get; set; }

        private Dictionary<string, ParameterVm> _originalParams;

        internal void ApplyChanges()
        {
            foreach (var parameter in Parameters)
            {
                parameter.ApplyChanges();
            }
        }

        internal IEnumerable<ReportParameter> GetParameters()
        {
            return Parameters.Select(p => p.Parameter);
        }
    }
}
