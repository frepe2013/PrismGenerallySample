using ServiceApp.ViewModels;
using System.Collections.Generic;

namespace ServiceApp.Services
{
    public interface IShelfService
    {
        IList<BookVm> GetList();

        BookVm Create(string inputTitle, string inputAuthor, Gender authorGender);

        void Update(BookVm viewModel, string inputTitle, string inputAuthor, Gender authorGender);
    }
}
