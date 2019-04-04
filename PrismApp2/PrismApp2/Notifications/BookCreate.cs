using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;
using PrismApp2.ViewModels;

namespace PrismApp2.Notifications
{
    public class BookCreate : Confirmation, IBookCreate
    {
        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public Gender AuthorGender { get; set; }
    }
}
