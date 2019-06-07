using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace DataGridApp.ViewModels
{
    public class BookVm : BindableBase
    {
        private static readonly Dictionary<Status, string> StatusDictionary = new Dictionary<Status, string>
        {
            {Status.CanLend, "貸出可能"},
            {Status.Lending, "貸出中"},
            {Status.NotReady, "準備中"},
        };

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
            set
            {
                _status = value;

                RaisePropertyChanged(nameof(DisplayStatus));
            }
        }

        public string DisplayStatus => StatusDictionary[Status];

        public DateTime? CheckoutDate
        {
            get => _checkoutDate;
            set
            {
                if (SetProperty(ref _checkoutDate, value))
                {
                    Status = GetStatus();
                }
            }
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set
            {
                if (SetProperty(ref _returnDate, value))
                {
                    Status = GetStatus();
                }
            }
        }

        private Status GetStatus()
        {
            if (CheckoutDate == null && ReturnDate == null)
            {
                return Status.CanLend;
            }

            if (CheckoutDate != null && ReturnDate == null)
            {
                return Status.Lending;
            }

            if (CheckoutDate != null && ReturnDate != null)
            {
                return Status.NotReady;
            }

            throw new Exception();
        }
    }
}
