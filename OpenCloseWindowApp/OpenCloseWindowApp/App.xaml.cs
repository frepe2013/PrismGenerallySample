using Prism.Ioc;
using OpenCloseWindowApp.Views;
using System.Windows;
using OpenCloseWindowApp.Services;

namespace OpenCloseWindowApp
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
            containerRegistry.Register<IRouteService, RouteService>();
        }
    }
}
