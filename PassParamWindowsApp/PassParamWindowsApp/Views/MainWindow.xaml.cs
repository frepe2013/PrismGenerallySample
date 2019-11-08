using System.Windows;
using PassParamWindowsApp.Core;
using Prism.Events;
using Prism.Ioc;

namespace PassParamWindowsApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IContainerExtension _container;
        private IEventAggregator _ea;

        public MainWindow(IContainerExtension container, IEventAggregator ea)
        {
            InitializeComponent();
            _container = container;
            _ea = ea;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new FeatureWindow();

            var text = MassageBox.Text;
            _ea.GetEvent<MessageSentEvent>().Publish(text);

            window.Show();
            this.Close();
        }
    }
}