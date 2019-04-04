using Prism.Mvvm;
using PrismApp2.Entities;

namespace PrismApp2.ViewModels
{
    public class BookVm : BindableBase
    {
        private bool _isEdited;

        public Book Book { get; }

        public string Title => Book.Title;

        public string Author => Book.Author;

        public Gender AuthorGender { get; }

        public string DisplayForList
        {
            get
            {
                if (_isEdited)
                {
                    return Title + " *";
                }

                return Title;
            }
        }

        public bool IsEdited
        {
            get => _isEdited;
            set
            {
                if (SetProperty(ref _isEdited, value))
                {
                    RaisePropertyChanged(nameof(DisplayForList));
                }
            }
        }

        public BookVm(Book book)
        {
            Book = book;
            switch (Book.AuthorGender)
            {
                case "F":
                    AuthorGender = Gender.Female;
                    break;
                case "M":
                    AuthorGender = Gender.Male;
                    break;
                default:
                    AuthorGender = Gender.Other;
                    break;
            }
        }
    }
}
