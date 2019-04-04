using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PrismApp2.Notifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PrismApp2.ViewModels
{
    public class CreateViewModel : BindableBase, IInteractionRequestAware, INotifyDataErrorInfo
    {
        private IBookCreate _notification;
        private string _bookTitle;
        private string _bookAuthor;
        private Gender _authorGender;
        private readonly ErrorsContainer<string> _errorsContainer;

        [Required]
        public string BookTitle
        {
            get => _bookTitle;
            set
            {
                if (SetProperty(ref _bookTitle, value))
                {
                    ValidateProperty(value);
                }
            }
        }

        [StringLength(20)]
        [RegularExpression("[^0-9]*")]
        public string BookAuthor
        {
            get => _bookAuthor;
            set
            {
                if (SetProperty(ref _bookAuthor, value))
                {
                    ValidateProperty(value);
                }
            }
        }

        public Gender AuthorGender
        {
            get => _authorGender;
            set => SetProperty(ref _authorGender, value);
        }

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public CreateViewModel()
        {
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);

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
            ValidateProperty(_bookTitle, nameof(BookTitle));
            ValidateProperty(_bookAuthor, nameof(BookAuthor));
            if (HasErrors) return;

            _notification.BookTitle = _bookTitle;
            _notification.BookAuthor = _bookAuthor;
            _notification.AuthorGender = _authorGender;

            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }

        private bool CanExecuteSaveCommand()
        {
            return !HasErrors;
        }

        private void ExecuteCancelCommand()
        {
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        public Action FinishInteraction { get; set; }

        public INotification Notification
        {
            get => _notification;
            set
            {
                SetProperty(ref _notification, (IBookCreate) value);
                BookTitle = "";
                BookAuthor = "";
                AuthorGender = Gender.Male;
                _errorsContainer.ClearErrors();
            }
        }

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