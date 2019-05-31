using ServiceApp.DAL;
using ServiceApp.Entities;
using ServiceApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ServiceApp.Services
{
    public class ShelfService : IShelfService
    {
        private IBookRepository _repository;

        public ShelfService(IBookRepository repository)
        {
            _repository = repository;
        }

        public IList<BookVm> GetList()
        {
            var bookList = _repository.FindAll();

            return bookList.Select(book => new BookVm(book)).ToList();
        }

        public BookVm Create(string inputTitle, string inputAuthor, Gender authorGender)
        {
            var book = new Book
            {
                Title = inputTitle,
                Author = inputAuthor,
                AuthorGender = authorGender.ToString()
            };

            var result = _repository.Insert(book);
            _repository.Save();

            return new BookVm(result);
        }

        public void Update(BookVm viewModel, string inputTitle, string inputAuthor, Gender authorGender)
        {
            viewModel.Model.Title = inputTitle;
            viewModel.Model.Author = inputAuthor;
            viewModel.Model.AuthorGender = authorGender.ToString();

            _repository.Update(viewModel.Model);
            _repository.Save();
        }
    }
}
