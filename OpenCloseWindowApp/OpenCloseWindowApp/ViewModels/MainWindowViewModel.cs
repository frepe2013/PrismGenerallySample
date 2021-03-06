﻿using System.ComponentModel;
using System.Windows;
using OpenCloseWindowApp.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace OpenCloseWindowApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRouteService _routeService;

        private string _title = "Prism Application";
        private string _username;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool CancelClose { get; set; } = true;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public DelegateCommand<CancelEventArgs> ClosingCommand { get; set; }

        public DelegateCommand<Window> OpenCommand { get; set; }

        public MainWindowViewModel(IRouteService routeService)
        {
            _routeService = routeService;

            ClosingCommand = new DelegateCommand<CancelEventArgs>(ExecuteClosing);
            OpenCommand = new DelegateCommand<Window>(ExecuteOpen);
        }

        private void ExecuteClosing(CancelEventArgs e)
        {
            if (CancelClose)
            {
                e.Cancel = true;
            }
        }

        private void ExecuteOpen(Window window)
        {
            _routeService.ShowBrandNewWindow(Username);

            CancelClose = false;
            window?.Close();
        }
    }
}
