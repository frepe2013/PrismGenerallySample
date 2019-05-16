using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BasicApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        private IDictionary<string, ICollection<string>> _errors = new Dictionary<string, ICollection<string>>();

        private int _id;
        private string _title;
        private string _author;

        public int Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                {
                    var messages = new List<string>();
                    if (string.IsNullOrEmpty(value))
                    {
                        messages.Add("Title is required");
                    }

                    _errors[nameof(Title)] = messages;

                    RaiseErrorsChanged();
                }
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (SetProperty(ref _author, value))
                {
                    var messages = new List<string>();
                    if (value.Length > 20)
                    {
                        messages.Add("Author is less than 20 characters");
                    }

                    if (Regex.IsMatch(value, "\\d"))
                    {
                        messages.Add("Author can not contain numbers");
                    }

                    _errors[nameof(Author)] = messages;

                    RaiseErrorsChanged();
                }
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        public DetailViewModel()
        {
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
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

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            if (!_errors.ContainsKey(propertyName))
            {
                return null;
            }

            if (!_errors[propertyName].Any())
            {
                return null;
            }

            return _errors[propertyName];
        }

        public bool HasErrors => _errors.Values.Any(collection => collection.Any());

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
