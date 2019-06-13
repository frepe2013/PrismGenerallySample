using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataGridApp.ViewModels
{
    public class BookVm : BindableBase, INotifyDataErrorInfo
    {
        private static readonly Dictionary<Status, string> StatusDictionary = new Dictionary<Status, string>
        {
            {Status.CanLend, "貸出可能"},
            {Status.Lending, "貸出中"},
            {Status.NotReady, "準備中"},
        };

        private readonly ErrorsContainer<string> _errors;

        private string _title;
        private string _author;
        private Status _status;
        private DateTime? _checkoutDate;
        private DateTime? _returnDate;

        [Required(ErrorMessage = "Title is required")]
        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                {
                    ValidateProperty(value);
                }
            }
        }

        [StringLength(20, ErrorMessage = "Author is less than 20 characters")]
        public string Author
        {
            get => _author;
            set
            {
                if (SetProperty(ref _author, value))
                {
                    ValidateProperty(value);
                }
            }
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

        public BookVm()
        {
            _errors = new ErrorsContainer<string>(RaiseErrorsChanged);
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

            //TODO Row全体エラーにする
            //_errors.SetErrors(nameof(DisplayStatus), new[] { "Logic Error" });　

            return Status;
        }

        protected void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            var context = new ValidationContext(this) { MemberName = propertyName };
            var validationErrors = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(value, context, validationErrors))
            {
                _errors.SetErrors(propertyName, validationErrors.Select(error => error.ErrorMessage));
            }
            else
            {
                _errors.ClearErrors(propertyName);
            }
        }

        private void RaiseErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName) => _errors.GetErrors(propertyName);

        public bool HasErrors => _errors.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
