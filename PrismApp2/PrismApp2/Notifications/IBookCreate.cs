using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;
using PrismApp2.Entities;
using PrismApp2.ViewModels;

namespace PrismApp2.Notifications
{
    public interface IBookCreate : IConfirmation
    {
        string BookTitle { get; set; }

        string BookAuthor { get; set; }

        Gender AuthorGender { get; set; }
    }
}
