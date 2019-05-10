using Prism.Mvvm;

namespace BindableBaseApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _author;

        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }
    }
}
