using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DelegateCommandApp.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private ObservableCollection<BookVm> _books;

        public ObservableCollection<BookVm> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public ListViewModel()
        {
            Books = new ObservableCollection<BookVm>(new List<BookVm>
            {
                new BookVm("Test-Driven Development", "Kent Beck"),
                new BookVm("The Healthy Programmer", "Joe Kutner"),
                new BookVm("Effective C#", "Bill Wagner"),
            });
        }
    }
}
