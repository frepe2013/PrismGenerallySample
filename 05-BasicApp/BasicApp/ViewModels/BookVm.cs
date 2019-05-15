using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicApp.Entities;

namespace BasicApp.ViewModels
{
    public class BookVm
    {
        private readonly Book _book;

        public int Id => _book.Id;

        public string Title => _book.Title;

        public string Author => _book.Author;

        public BookVm(Book book)
        {
            _book = book;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
