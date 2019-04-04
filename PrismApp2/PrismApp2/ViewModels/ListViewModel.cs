using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using PrismApp2.Notifications;
using PrismApp2.Services;
using System.Collections.ObjectModel;

namespace PrismApp2.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        private IShelfService _service;

        private ObservableCollection<BookVm> _books = new ObservableCollection<BookVm>();

        private string _keyword;

        private BookVm _targetBook;

        private string _message;

        public ObservableCollection<BookVm> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value);
        }

        public BookVm TargetBook
        {
            get => _targetBook;
            set => SetProperty(ref _targetBook, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand SearchCommand { get; set; }

        public DelegateCommand<BookVm> BookSelectedCommand { get; set; }

        public InteractionRequest<IBookCreate> CreateRequest { get; set; }

        public DelegateCommand CreateCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        public ListViewModel(IRegionManager regionManager, IShelfService service)
        {
            _regionManager = regionManager;
            _service = service;

            SearchCommand = new DelegateCommand(ExecuteSearchCommand);
            BookSelectedCommand = new DelegateCommand<BookVm>(ExecuteBookSelectedCommand);

            CreateRequest = new InteractionRequest<IBookCreate>();
            CreateCommand = new DelegateCommand(ExecuteCreateCommand);

            DeleteCommand = new DelegateCommand(ExecuteDeleteCommand);

            LoadList();
        }

        private void ExecuteSearchCommand()
        {
            Message = "";
            TargetBook = null;
            Books.Clear();
            LoadList(_keyword);

            _regionManager.Regions["RightRegion"].RemoveAll();
        }

        private void LoadList(string keyword = null)
        {
            var list = _service.Search(keyword);
            Books.AddRange(list);
        }

        private void ExecuteBookSelectedCommand(BookVm data)
        {
            if (data == null) return;

            var parameters = new NavigationParameters { { "book", data } };
            _regionManager.RequestNavigate("RightRegion", "Detail", parameters);
        }

        private void ExecuteCreateCommand()
        {
            CreateRequest.Raise(
                new BookCreate
                {
                    Title = "Book Create",
                },
                c =>
                {
                    if (c.Confirmed)
                    {
                        //DB登録処理
                        var bookVm = _service.Create(c);

                        ExecuteSearchCommand();

                        Message = $"登録完了 タイトル:{bookVm.Title}";
                    }
                });
        }

        private void ExecuteDeleteCommand()
        {
            var title = TargetBook.Title;
            // Delete Record
            _service.Delete(TargetBook);

            ExecuteSearchCommand();

            Message = $"削除完了 タイトル:{title}";
        }
    }
}
