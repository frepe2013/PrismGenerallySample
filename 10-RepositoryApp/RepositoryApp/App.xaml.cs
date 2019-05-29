using System.Data.Entity;
using RepositoryApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using RepositoryApp.DAL;
using RepositoryApp.Entities;

namespace RepositoryApp
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
