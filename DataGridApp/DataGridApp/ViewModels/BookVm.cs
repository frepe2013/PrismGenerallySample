using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DataGridApp.ViewModels
{
    public class BookVm : BindableBase
    {
        private string _title;
        private string _author;
        private Status _status;
        private DateTime? _checkoutDate;
        private DateTime? _returnDate;

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

        public Status Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public DateTime? CheckoutDate
        {
            get => _checkoutDate;
            set => SetProperty(ref _checkoutDate, value);
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set => SetProperty(ref _returnDate, value);
        }
    }
}
