using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;

namespace RegionNavigationApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware
    {
        private string _displayText;

        public string DisplayText
        {
            get => _displayText;
            set => SetProperty(ref _displayText, value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Message1"] is string message)
            {
                DisplayText = message;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
