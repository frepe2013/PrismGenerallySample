using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegionNavigationApp.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private string _inputText = "Say Hello!";

        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }
    }
}
