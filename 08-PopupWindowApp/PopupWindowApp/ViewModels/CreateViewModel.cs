using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PopupWindowApp.ViewModels
{
    public class CreateViewModel : BindableBase, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<string> _errors;

        private string _inputTitle;
        private string _inputAuthor;

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

        public DelegateCommand SaveCommand { get; set; }

        public CreateViewModel()
        {
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand)
                .ObservesProperty(() => InputTitle).ObservesProperty(() => InputAuthor);

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
            throw new NotImplementedException();
        }

        private bool CanExecuteSaveCommand()
        {
            return true;
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