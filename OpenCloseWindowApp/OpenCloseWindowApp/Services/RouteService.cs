using System;
using System.Collections.Generic;
using System.Text;
using OpenCloseWindowApp.Views;
using Prism.Ioc;

namespace OpenCloseWindowApp.Services
{
    public class RouteService : IRouteService
    {
        private readonly IContainerExtension _container;

        public RouteService(IContainerExtension container)
        {
            _container = container;
        }

        public void ShowBrandNewWindow()
        {
            var brandNewWindow = _container.Resolve<BrandNewWindow>();
            brandNewWindow.Show();
        }
    }
}
