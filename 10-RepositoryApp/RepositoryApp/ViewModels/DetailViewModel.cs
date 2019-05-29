using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RepositoryApp.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RepositoryApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<string> _errors;

        private int _id;
        private string _inputTitle;
        private string _inputAuthor;
        private Gender _authorGender;
        private BookVm _bookVm;
        private IBookRepository _repository;

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

                    if (CanEdit)
                    {
                        _bookVm.IsEdited = IsObjectChanged();
                    }
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

                    if (CanEdit)
                    {
                        _bookVm.IsEdited = IsObjectChanged();
                    }
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
                    if (CanEdit)
                    {
                        _bookVm.IsEdited = IsObjectChanged();
                    }
                }
            }
        }

        public bool CanEdit { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public DetailViewModel(IBookRepository repository)
        {
            _repository = repository;

            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand)
                .ObservesProperty(() => InputTitle)
                .ObservesProperty(() => InputAuthor)
                .ObservesProperty(() => AuthorGender);

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
            _bookVm.Model.AuthorGender = AuthorGender.ToString();

            _repository.Update(_bookVm.Model);
            _repository.Save();

            _bookVm.IsEdited = false;
            SaveCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecuteSaveCommand()
        {
            return (_bookVm?.IsEdited ?? false) && !HasErrors;
        }

        private bool IsObjectChanged()
        {
            if (InputTitle != _bookVm.Title)
            {
                return true;
            }

            if (InputAuthor != _bookVm.Author)
            {
                return true;
            }

            if (AuthorGender != _bookVm.AuthorGender)
            {
                return true;
            }

            return false;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (IsNavigationTarget(navigationContext)) return;

            var selectedBook = (BookVm)navigationContext.Parameters["book"];
            Id = selectedBook.Id;
            InputTitle = selectedBook.Title;
            InputAuthor = selectedBook.Author;
            AuthorGender = selectedBook.AuthorGender;
            _bookVm = selectedBook;
            CanEdit = true;
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
