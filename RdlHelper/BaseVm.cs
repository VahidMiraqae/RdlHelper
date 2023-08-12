﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RdlHelper
{
    internal class BaseVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}