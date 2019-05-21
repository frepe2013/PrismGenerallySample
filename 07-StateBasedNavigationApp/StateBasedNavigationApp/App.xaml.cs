using System.Data.Entity;
using StateBasedNavigationApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using StateBasedNavigationApp.Entities;

namespace StateBasedNavigationApp
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
            containerRegistry.RegisterForNavigation<Detail>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new DbInitializer());
            base.OnStartup(e);
        }
    }
}
