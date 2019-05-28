using System.Data.Entity;
using PopupWindowApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using PopupWindowApp.Entities;

namespace PopupWindowApp
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
            containerRegistry.RegisterForNavigation<Create>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new DbInitializer());
            base.OnStartup(e);
        }
    }
}
