using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IValueConverterApp.ViewModels;
using Prism.Interactivity.InteractionRequest;

namespace IValueConverterApp.Notifications
{
    public interface IBookCreate : IConfirmation
    {
        string BookTitle { get; set; }

        string BookAuthor { get; set; }

        Gender AuthorGender { get; set; }
    }
}
