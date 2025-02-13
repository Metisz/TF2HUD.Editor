﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HUDEditor.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Invoke a WPF Binding update of a property.
        /// </summary>
        /// <param name="propertyChanged">Name of property to update</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            MainWindow.Logger.Info($"OnPropertyChanged: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
        }
    }
}
