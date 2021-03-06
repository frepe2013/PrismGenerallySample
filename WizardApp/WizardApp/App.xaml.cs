﻿using Prism.Ioc;
using WizardApp.Views;
using System.Windows;

namespace WizardApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Second>();
            containerRegistry.RegisterForNavigation<Third>();
        }
    }
}
