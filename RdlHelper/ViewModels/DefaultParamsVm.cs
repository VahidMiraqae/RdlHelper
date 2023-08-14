using RdlHelper.Models;
using RdlHelper.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RdlHelper.Views
{
    internal class DefaultParamsVm : BaseVm
    {
        public DefaultParamsVm(IEnumerable<RdlParameter> parameters) 
        {
            Items = parameters.Select(p => new ReportParam(p.GetParameterName(), p.GetParameterType(), p.GetDefaultValues())).ToList();
            
        }
        public List<ReportParam> Items { get; set; }
    }
}
