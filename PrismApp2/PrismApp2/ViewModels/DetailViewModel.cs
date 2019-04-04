using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismApp2.Services;

namespace PrismApp2.ViewModels
{
    public enum Gender
    {
        Male, Female, Other
    }

    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        private IShelfService _service;
        private BookVm _book;
        private string _title;
        private string _author;
        private Gender _authorGender;
        private readonly ErrorsContainer<string> _errorsContainer;

        public BookVm Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }

        [Required]
        public string Title
        {
            get => _title;
            set
            {
                var init = _title == null;
                if (SetProperty(ref _title, value))
                {
                    if (init) return;
                    ValidateProperty(value);

                    //if (_book == null) return;

                    _book.IsEdited = !IsObjectChanged();
                }
            }
        }

        [StringLength(20)]
        [RegularExpression("[^0-9]*")]
        public string Author
        {
            get => _author;
            set
            {
                var init = _author == null;
                if (SetProperty(ref _author, value))
                {
                    if (init) return;
                    ValidateProperty(value);

                    //if (_book == null) return;

                    _book.IsEdited = !IsObjectChanged();
                }
            }
        }

        public Gender AuthorGender
        {
            get => _authorGender;
            set
            {
                if (SetProperty(ref _authorGender, value))
                {
                    _book.IsEdited = !IsObjectChanged();
                }
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        public DetailViewModel(IShelfService service)
        {
            _service = service;

            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand).ObservesProperty(() => Book.IsEdited);

            _errorsContainer = new ErrorsContainer<string>(RaiseErrorsChanged);
            ErrorsChanged += (s, a) => { SaveCommand.RaiseCanExecuteChanged(); };
        }

        protected void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            var context = new ValidationContext(this) {MemberName = propertyName};
            var validationErrors = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(value, context, validationErrors))
            {
                _errorsContainer.SetErrors(propertyName, validationErrors.Select(error => error.ErrorMessage));
            }
            else
            {
                _errorsContainer.ClearErrors(propertyName);
            }
        }

        private void ExecuteSaveCommand()
        {
            // Update Database
            _service.Update(this);

            // Update UI
            _book.IsEdited = false;
        }

        private bool CanExecuteSaveCommand()
        {
            return (_book?.IsEdited ?? false) && !HasErrors;
        }

        private bool IsObjectChanged()
        {
            return _book.Title == _title && _book.Author == _author && _book.AuthorGender == _authorGender;
        }

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (IsNavigationTarget(navigationContext)) return;

            if (navigationContext.Parameters["book"] is BookVm selectedBook)
            {
                Book = selectedBook;
                Title = Book.Title;
                Author = Book.Author;
                AuthorGender = Book.AuthorGender;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["book"] is BookVm selectedBook)
            {
                return _book != null && _book.Title == selectedBook.Title;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion //Navigation

        #region INotifyDataErrorInfo

        //_errorsContainerの状態が変わると呼び出される
        //ErrorsChangedイベントを発行する。イベントを発行しなくても、HasErrors/GetErrorsが実装されていればエラーメッセージは表示される。
        private void RaiseErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName) => _errorsContainer.GetErrors(propertyName);

        public bool HasErrors => _errorsContainer.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion //INotifyDataErrorInfo
    }
}