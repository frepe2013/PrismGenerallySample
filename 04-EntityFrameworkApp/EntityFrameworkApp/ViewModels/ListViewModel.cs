using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EntityFrameworkApp.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        private ObservableCollection<BookVm> _books;

        public ObservableCollection<BookVm> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public DelegateCommand<BookVm> BookSelectedCommand { get; set; }

        public ListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Books = new ObservableCollection<BookVm>(new List<BookVm>
            {
                new BookVm("Test-Driven Development", "Kent Beck"),
                new BookVm("The Healthy Programmer", "Joe Kutner"),
                new BookVm("Effective C#", "Bill Wagner"),
            });

            BookSelectedCommand = new DelegateCommand<BookVm>(ExecuteBookSelectedCommand);
        }

        private void ExecuteBookSelectedCommand(BookVm data)
        {
            if (data == null) return;

            var parameters = new NavigationParameters { { "book", data } };
            _regionManager.RequestNavigate("RightRegion", "Detail", parameters);
        }
    }
}
