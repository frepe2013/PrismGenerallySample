using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCloseWindowApp.ViewModels
{
    public class BrandNewWindowViewModel : BindableBase
    {
        private string _title = "Brand New Window";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public BrandNewWindowViewModel()
        {
        }
    }
}
