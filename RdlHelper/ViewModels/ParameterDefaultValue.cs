using RdlHelper.ViewModels;

namespace RdlHelper.Views
{
    internal class ParameterDefaultValue : BaseVm
    {
        public ParameterDefaultValue(string value)
        {
            DefaultValue = value;
        }
        public string DefaultValue { get; set; }
    }
}
