using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;

namespace PopupWindowApp.Notifications
{
    public interface IBookCreate : IConfirmation
    {
        string BookTitle { get; set; }

        string BookAuthor { get; set; }
    }
}
