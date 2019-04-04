using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismApp2.DAL;
using PrismApp2.Entities;
using PrismApp2.Notifications;
using PrismApp2.ViewModels;

namespace PrismApp2.Services
{
    public class ShelfService : IShelfService
    {
        private IBookRepository _repository;

        public ShelfService(IBookRepository repository)
        {
            _repository = repository;
        }

        public IList<BookVm> Search(string keyword)
        {
            var books = _repository.Search(keyword);

            return books.Select(book => new BookVm(book)).ToList();
        }

        public BookVm Create(IBookCreate notification)
        {
            var book = new Book
            {
                Title = notification.BookTitle,
                Author = notification.BookAuthor,
                AuthorGender = notification.AuthorGender.ToString().Substring(0, 1)
            };

            var result = _repository.Insert(book);
            _repository.Save();

            return new BookVm(result);
        }

        public void Update(DetailViewModel viewModel)
        {
            var bookVm = viewModel.Book;
            var entity = bookVm.Book;
            entity.Title = viewModel.Title;
            entity.Author = viewModel.Author;
            entity.AuthorGender = viewModel.AuthorGender.ToString().Substring(0, 1);

            _repository.Update(entity);
            _repository.Save();
        }

        public void Delete(BookVm vm)
        {
            _repository.Delete(vm.Book);
            _repository.Save();
        }
    }
}
