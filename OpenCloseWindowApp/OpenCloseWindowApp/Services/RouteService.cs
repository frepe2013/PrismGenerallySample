using System;
using System.Collections.Generic;
using System.Text;
using OpenCloseWindowApp.Events;
using OpenCloseWindowApp.Views;
using Prism.Events;
using Prism.Ioc;

namespace OpenCloseWindowApp.Services
{
    public class RouteService : IRouteService
    {
        private readonly IContainerExtension _container;
        private IEventAggregator _ea;

        public RouteService(IContainerExtension container, IEventAggregator ea)
        {
            _container = container;
            _ea = ea;
        }

        public void ShowBrandNewWindow(string username)
        {
            var brandNewWindow = _container.Resolve<BrandNewWindow>();

            _ea.GetEvent<MessageSentEvent>().Publish(username);

            brandNewWindow.Show();
        }
    }
}
