using PrismApp2.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PrismApp2.DAL
{
    public class BookRepository : BaseRepository<ShelfContext>, IBookRepository
    {
        protected override ShelfContext Context { get; }

        public BookRepository(ShelfContext context)
        {
            Context = context;
        }

        public IList<Book> Search(string keyword)
        {
            IQueryable<Book> query = Context.Books;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.Title.StartsWith(keyword));
            }

            return query.ToList();
        }

        public Book Insert(Book model)
        {
            return Context.Books.Add(model);
        }

        public void Update(Book model)
        {
            var entry = Context.Entry(model);
            entry.State = EntityState.Modified;
        }

        public void Delete(Book model)
        {
            var entry = Context.Entry(model);
            entry.State = EntityState.Deleted;
        }
    }
}