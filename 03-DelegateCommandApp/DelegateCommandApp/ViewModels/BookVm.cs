using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DelegateCommandApp.ViewModels
{
    public class BookVm
    {
        public string Title { get; }

        public string Author { get; }

        public BookVm(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
