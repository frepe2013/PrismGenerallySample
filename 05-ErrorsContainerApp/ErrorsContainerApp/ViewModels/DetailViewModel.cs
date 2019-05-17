using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Prism.Regions;

namespace ErrorsContainerApp.ViewModels
{
    public class DetailViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
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
            set => SetProperty(ref _title, value);
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
            throw new NotImplementedException();
        }

        public bool HasErrors => false;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
