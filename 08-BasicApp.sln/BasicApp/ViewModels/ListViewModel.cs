﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Regions;
using BasicApp.Entities;
using BasicApp.Views;

namespace BasicApp.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private IRegionManager _regionManager;

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

        public DelegateCommand CreateCommand { get; set; }

        public ListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Books = new ObservableCollection<BookVm>();
            using (var context = new ShelfContext())
            {
                var bookList = context.Books.ToList();
                var vms = bookList.Select(book => new BookVm(book));
                Books.AddRange(vms);
            }

            BookSelectedCommand = new DelegateCommand<BookVm>(ExecuteBookSelectedCommand);
            CreateCommand = new DelegateCommand(ExecuteCreateCommand);
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
            var create = new Create { Title = "Book Create" };
            var ok = create.ShowDialog() ?? false;
            if (ok)
            {
                CreateViewModel viewModel = (CreateViewModel) create.DataContext;
                Message = $"Insert Complete! Title:{viewModel.InputTitle}";
            }
        }
    }
}
