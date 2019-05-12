using EntityFrameworkApp.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

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
            Books = new ObservableCollection<BookVm>();
            using (var context = new ShelfContext())
            {
                var bookList = context.Books.ToList();
                var vms = bookList.Select(book => new BookVm(book.Title, book.Author));
                Books.AddRange(vms);
            }

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
