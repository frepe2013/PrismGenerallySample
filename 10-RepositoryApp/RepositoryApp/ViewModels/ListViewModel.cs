﻿using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using RepositoryApp.DAL;
using RepositoryApp.Entities;
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
        private BookVm _targetBook;
        private string _message;

        public ObservableCollection<BookVm> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
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
            TargetBook = null; //SelectedItemをクリアしないと、リストの表示を更新する時にエラーになる

            var bookList = _repository.FindAll();

            var vms = bookList.Select(book => new BookVm(book));
            Books = new ObservableCollection<BookVm>(vms);

            _regionManager.Regions["RightRegion"].RemoveAll(); //これをしないとRightRegionに表示した値を編集してもリストに反映されない
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
                        //DB登録処理
                        var book = new Book
                        {
                            Title = c.BookTitle,
                            Author = c.BookAuthor,
                            AuthorGender = c.AuthorGender.ToString()
                        };

                        _repository.Insert(book);
                        _repository.Save();

                        Message = $"Insert Complete! Title:{c.BookTitle}";

                        //表示更新
                        LoadList();
                    }
                });
        }
    }
}
