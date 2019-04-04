using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismApp2.Notifications;
using PrismApp2.ViewModels;

namespace PrismApp2.Services
{
    public interface IShelfService
    {
        IList<BookVm> Search(string keyword);

        BookVm Create(IBookCreate notification);

        void Update(DetailViewModel viewModel);

        void Delete(BookVm vm);
    }
}
