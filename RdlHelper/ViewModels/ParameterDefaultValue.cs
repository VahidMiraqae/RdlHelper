using RdlHelper.ViewModels;

namespace RdlHelper.Views
{
    internal class ParameterDefaultValue : BaseVm
    {
        public ParameterDefaultValue(string title)
        {
            DefaultValue = title;
        }
        public string DefaultValue { get; set; }
    }
}
