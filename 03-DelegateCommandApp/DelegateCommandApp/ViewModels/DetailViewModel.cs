using Prism.Mvvm;
using Prism.Regions;

namespace DelegateCommandApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware
    {
        private string _title;
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

        public DetailViewModel()
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (IsNavigationTarget(navigationContext)) return;

            if (navigationContext.Parameters["book"] is BookVm selectedBook)
            {
                Title = selectedBook.Title;
                Author = selectedBook.Author;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["book"] is BookVm selectedBook)
            {
                return Title == selectedBook.Title;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
