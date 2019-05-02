using Prism.Mvvm;

namespace BindableBaseApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MainWindow";
        private string _author;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }
    }
}
