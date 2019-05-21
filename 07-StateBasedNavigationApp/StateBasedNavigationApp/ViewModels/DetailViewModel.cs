using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using StateBasedNavigationApp.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StateBasedNavigationApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<string> _errors;

        private int _id;
        private string _inputTitle;
        private string _inputAuthor;
        private BookVm _bookVm;

        public int Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

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
            _bookVm.Model.Title = InputTitle;
            _bookVm.Model.Author = InputAuthor;

            using (var context = new ShelfContext())
            {
                var entry = context.Entry(_bookVm.Model);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }
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
            InputTitle = selectedBook.Title;
            InputAuthor = selectedBook.Author;
            _bookVm = selectedBook;
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
