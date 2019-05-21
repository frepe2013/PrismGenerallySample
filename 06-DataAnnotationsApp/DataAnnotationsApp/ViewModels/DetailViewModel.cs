using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataAnnotationsApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<string> _errors;

        private int _id;
        private string _title;
        private string _author;

        public int Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

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
        [RegularExpression("[^0-9]*", ErrorMessage = "Author can not contain numbers")]
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

        public DelegateCommand SaveCommand { get; set; }

        public DetailViewModel()
        {
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand);

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
        }

        private bool CanExecuteSaveCommand()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (IsNavigationTarget(navigationContext)) return;

            var selectedBook = (BookVm)navigationContext.Parameters["book"];
            Id = selectedBook.Id;
            Title = selectedBook.Title;
            Author = selectedBook.Author;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["book"] is BookVm selectedBook)
            {
                return Id == selectedBook.Id;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // TitleBox ToolTip&Background clear.
            //
            //var enumerator = navigationContext.NavigationService.Region.ActiveViews.GetEnumerator();
            //enumerator.MoveNext();
            //var view = (Detail)enumerator.Current;
            //view.TitleBox.ToolTip = string.Empty;
            //view.TitleBox.Background = null;
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
