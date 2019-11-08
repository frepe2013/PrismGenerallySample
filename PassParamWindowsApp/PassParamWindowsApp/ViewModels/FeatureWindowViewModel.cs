using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using PassParamWindowsApp.Core;
using Prism.Events;

namespace PassParamWindowsApp.ViewModels
{
    public class FeatureWindowViewModel : BindableBase
    {
        private IEventAggregator _ea;

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public FeatureWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;

            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(string msg)
        {
            Message = msg;
        }
    }
}