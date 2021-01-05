using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ProgressBarApp2.ViewModels
{
    public class PValue : BindableBase
    {
        private int _progressValue;

        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }
    }
}
