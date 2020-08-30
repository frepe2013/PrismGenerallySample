using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenCloseWindowApp.Events;
using Prism.Events;

namespace OpenCloseWindowApp.ViewModels
{
    public class BrandNewWindowViewModel : BindableBase
    {
        private IEventAggregator _ea;

        private string _title = "Brand New Window";
        private string _displayName;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { SetProperty(ref _displayName, value); }
        }

        public BrandNewWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;

            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(string msg)
        {
            DisplayName = $"Hello, {msg}";
        }
    }
}
