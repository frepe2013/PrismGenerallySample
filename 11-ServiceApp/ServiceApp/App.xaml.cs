using System.Data.Entity;
using ServiceApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using ServiceApp.DAL;
using ServiceApp.Entities;

namespace ServiceApp
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

            containerRegistry.Register<ShelfContext>();
            containerRegistry.Register<IBookRepository, BookRepository>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new DbInitializer());
            base.OnStartup(e);
        }
    }
}
