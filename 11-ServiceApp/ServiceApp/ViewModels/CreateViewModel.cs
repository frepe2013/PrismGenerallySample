﻿using ServiceApp.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ServiceApp.ViewModels
{
    public class CreateViewModel : BindableBase, IInteractionRequestAware, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<string> _errors;

        private string _inputTitle;
        private string _inputAuthor;
        private Gender _authorGender;
        private IBookCreate _notification;

        [Required(ErrorMessage = "Title is required")]
        public string InputTitle
        {
            get => _inputTitle;
            set
            {
                if (SetProperty(ref _inputTitle, value))
                {
                    ValidateProperty(value);
                }
            }
        }

        [StringLength(20, ErrorMessage = "Author is less than 20 characters")]
        [RegularExpression("[^0-9]*", ErrorMessage = "Author can not contain numbers")]
        public string InputAuthor
        {
            get => _inputAuthor;
            set
            {
                if (SetProperty(ref _inputAuthor, value))
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
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand)
                .ObservesProperty(() => InputTitle).ObservesProperty(() => InputAuthor);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);

            _errors = new ErrorsContainer<string>(RaiseErrorsChanged);
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

        private void ExecuteSaveCommand()
        {
            ValidateProperty(_inputTitle, nameof(InputTitle));
            ValidateProperty(_inputAuthor, nameof(InputAuthor));
            if (HasErrors) return;

            _notification.BookTitle = InputTitle;
            _notification.BookAuthor = InputAuthor;
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
            set => SetProperty(ref _notification, (IBookCreate)value);
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