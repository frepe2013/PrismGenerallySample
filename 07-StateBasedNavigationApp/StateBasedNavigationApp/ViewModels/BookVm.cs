using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateBasedNavigationApp.Entities;

namespace StateBasedNavigationApp.ViewModels
{
    public class BookVm
    {
        internal Book Model { get; }

        public int Id => Model.Id;

        public string Title => Model.Title;

        public string Author => Model.Author;

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
