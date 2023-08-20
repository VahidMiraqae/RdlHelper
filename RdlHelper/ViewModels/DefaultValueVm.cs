using RdlHelper.ViewModels;

namespace RdlHelper.Views
{
    internal class DefaultValueVm : BaseVm
    {
        private string _defaultValue;

        public DefaultValueVm(string value)
        {
            _originalValue = value;
            DefaultValue = value;
        }

        private string _originalValue;

        public string DefaultValue { get => _defaultValue; set { _defaultValue = value; OnPropChanged(); } }
        public bool Chagned => _originalValue != _defaultValue;
    }
}
