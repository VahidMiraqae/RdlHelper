using RdlHelper.ViewModels;

namespace RdlHelper.Views
{
    internal class ParameterDefaultValue : BaseVm
    {
        private string _defaultValue;

        public ParameterDefaultValue(string value)
        {
            _originalValue = value;
            DefaultValue = value;
        }

        private string _originalValue;

        public string DefaultValue { get => _defaultValue; set { _defaultValue = value; OnPropChanged(); } }
        public bool Chagned => _originalValue != _defaultValue;
    }
}
