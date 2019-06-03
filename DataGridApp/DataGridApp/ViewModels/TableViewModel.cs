using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataGridApp.ViewModels
{
    public class TableViewModel : BindableBase
    {
        private ObservableCollection<BookVm> _books;

        public ObservableCollection<BookVm> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public TableViewModel()
        {
            Books = new ObservableCollection<BookVm>
            {
                new BookVm {Title = "Test-Driven Development", Author = "Kent Beck", Status = Status.Lending, CheckoutDate = new DateTime(2019,5,1), ReturnDate = null},
                new BookVm {Title = "The Healthy Programmer", Author = "Joe Kutner", Status = Status.CanLend, CheckoutDate = null,ReturnDate = null},
                new BookVm {Title = "Effective C#", Author = "Bill Wagner", Status = Status.NotReady, CheckoutDate = new DateTime(2019,4,1), ReturnDate = new DateTime(2019,5,1)}
            };
        }
    }
}
