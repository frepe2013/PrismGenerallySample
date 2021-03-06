﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceApp.ViewModels;
using Prism.Interactivity.InteractionRequest;

namespace ServiceApp.Notifications
{
    public interface IBookCreate : IConfirmation
    {
        string BookTitle { get; set; }

        string BookAuthor { get; set; }

        Gender AuthorGender { get; set; }
    }
}
