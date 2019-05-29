using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using RepositoryApp.DAL;
using RepositoryApp.Notifications;
using System.Collections.ObjectModel;
using System.Linq;

namespace RepositoryApp.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IBookRepository _repository;

        private ObservableCollection<BookVm> _books;
        private string _message;

        public ObservableCollection<BookVm> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand<BookVm> BookSelectedCommand { get; set; }

        public InteractionRequest<IBookCreate> CreateRequest { get; set; }

        public DelegateCommand CreateCommand { get; set; }

        public ListViewModel(IRegionManager regionManager, IBookRepository repository)
        {
            _regionManager = regionManager;
            _repository = repository;

            BookSelectedCommand = new DelegateCommand<BookVm>(ExecuteBookSelectedCommand);

            CreateRequest = new InteractionRequest<IBookCreate>();
            CreateCommand = new DelegateCommand(ExecuteCreateCommand);

            LoadList();
        }

        private void LoadList()
        {
            var bookList = _repository.FindAll();

            var vms = bookList.Select(book => new BookVm(book));
            Books = new ObservableCollection<BookVm>(vms);
        }

        private void ExecuteBookSelectedCommand(BookVm data)
        {
            if (data == null) return;

            var parameters = new NavigationParameters { { "book", data } };
            _regionManager.RequestNavigate("RightRegion", "Detail", parameters);
        }

        private void ExecuteCreateCommand()
        {
            Message = string.Empty;
            CreateRequest.Raise(
                new BookCreate
                {
                    Title = "Book Create"
                },
                c =>
                {
                    if (c.Confirmed)
                    {
                        Message = $"Insert Complete! Title:{c.BookTitle}";
                    }
                });
        }
    }
}
