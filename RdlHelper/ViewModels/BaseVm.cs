using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RdlHelper.ViewModels
{
    internal class BaseVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropChanged( [CallerMemberName] string propertyName = "", params string[] propertyNames)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            foreach (var property in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}