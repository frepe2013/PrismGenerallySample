using System.Windows.Controls;
using Prism.Regions;

namespace RegionNavigationApp.Views
{
    /// <summary>
    /// Interaction logic for List
    /// </summary>
    public partial class List : UserControl
    {
        private readonly IRegionManager _regionManager;

        public List(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var parameters = new NavigationParameters { { "Message1", Message1.Text } };
            _regionManager.RequestNavigate("RightRegion", "Detail", parameters);
        }
    }
}
