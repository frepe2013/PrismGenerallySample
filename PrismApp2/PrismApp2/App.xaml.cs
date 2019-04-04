using Prism.Ioc;
using PrismApp2.DAL;
using PrismApp2.Entities;
using PrismApp2.Services;
using PrismApp2.Views;
using System.Data.Entity;
using System.Windows;

namespace PrismApp2
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
            containerRegistry.Register<ShelfContext>();
            containerRegistry.Register<IShelfService, ShelfService>();
            containerRegistry.Register<IBookRepository, BookRepository>();
            containerRegistry.RegisterForNavigation<Detail>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new DbInitializer());
            base.OnStartup(e);
        }
    }
}