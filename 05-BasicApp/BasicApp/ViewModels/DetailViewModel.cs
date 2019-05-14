using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BasicApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        private IDictionary<string, ICollection<string>> _errors = new Dictionary<string, ICollection<string>>();

        private string _title;
        private string _author;

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
            set => SetProperty(ref _author, value);
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
            Title = selectedBook.Title;
            Author = selectedBook.Author;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["book"] is BookVm selectedBook)
            {
                return Title == selectedBook.Title;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
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
