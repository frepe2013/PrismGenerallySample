using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ServiceApp.Entities;

namespace ServiceApp.ViewModels
{
    public class BookVm : BindableBase
    {
        private bool _isEdited;

        internal Book Model { get; }

        public int Id => Model.Id;

        public string Title => Model.Title;

        public string Author => Model.Author;

        public Gender AuthorGender
        {
            get
            {
                if (Enum.TryParse(Model.AuthorGender, out Gender gender))
                {
                    return gender;
                }
                else
                {
                    return Gender.Male;
                }
            }
        }

        public string TitleForList
        {
            get
            {
                if (IsEdited)
                {
                    return Title + " *";
                }
                else
                {
                    return Title;
                }
            }
        }

        public bool IsEdited
        {
            get => _isEdited;
            set
            {
                if (value == _isEdited)
                {
                    return;
                }

                _isEdited = value;

                RaisePropertyChanged(nameof(TitleForList));
            }
        }

        public BookVm(Book book)
        {
            Model = book;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
